using BookStorage.Application.Extensions;
using BookStorage.Infrastructure.Extensions;
using BookStorage.WebApi.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Services.AddControllers()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});

	builder.Services.AddHealthChecks()
		.AddSqlServer(
			builder.Configuration.GetConnectionString("DefaultConnection"),
			name: "SQL Server",
			failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy
		);

	builder.Services.AddJwtAuthentication(builder.Configuration);

	builder.Services.AddApplicationServices();
	builder.Services.AddApplicationRepository();

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddAutoMapper(typeof(Program));

	builder.Services.AddApiVersioning(options =>
	{
		options.DefaultApiVersion = new ApiVersion(1, 0);
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ReportApiVersions = true;
	});

	builder.Services.AddVersionedApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'VVV";
		options.SubstituteApiVersionInUrl = true;
	});

	builder.Services.AddSwaggerGen(options =>
	{
		var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
		var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
		options.IncludeXmlComments(xmlPath);
		options.UseInlineDefinitionsForEnums();
	});

	builder.Services.AddRateLimiter(options =>
	{
		options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
		{
			var clientIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

			return RateLimitPartition.GetFixedWindowLimiter(clientIp, _ => new FixedWindowRateLimiterOptions
			{
				PermitLimit = 10,
				Window = TimeSpan.FromSeconds(10),
				QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
				QueueLimit = 5
			});
		});

		options.RejectionStatusCode = 429;
	});

	builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

	Log.Logger = new LoggerConfiguration()
		.WriteTo.Console()
		.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
		.Enrich.FromLogContext()
		.MinimumLevel.Debug()
		.CreateLogger();

	builder.Host.UseSerilog();

	var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();

		var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

		app.UseSwaggerUI(options =>
		{
			foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
			{
				options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
					$"BookStorage API {description.GroupName.ToUpperInvariant()}");
			}
		});
	}

	app.Use(async (context, next) =>
	{
		try
		{
			await next();
		}
		catch (FluentValidation.ValidationException ex)
		{
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/json";

			var errors = ex.Errors
				.GroupBy(e => e.PropertyName)
				.ToDictionary(
					g => g.Key,
					g => g.Select(e => e.ErrorMessage).ToArray()
				);

			var result = System.Text.Json.JsonSerializer.Serialize(new { errors });
			await context.Response.WriteAsync(result);
		}
	});

	app.UseHttpsRedirection();

	app.UseRateLimiter();

	app.UseAuthorization();

	app.MapControllers();

	app.MapHealthChecks("/health", new HealthCheckOptions
	{
		ResponseWriter = async (context, report) =>
		{
			context.Response.ContentType = "application/json";
			var result = JsonSerializer.Serialize(new
			{
				status = report.Status.ToString(),
				checks = report.Entries.Select(e => new
				{
					name = e.Key,
					status = e.Value.Status.ToString(),
					duration = e.Value.Duration.ToString()
				})
			});
			await context.Response.WriteAsync(result);
		}
	});

	Log.Information("Starting up BookStorage API");
	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application startup failed");
}
