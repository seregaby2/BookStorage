using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Auth;
using BookStorage.Infrastructure.Data.Repositories;
using BookStorage.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStorage.WebApi.Extensions;

public static class AuthenticationExtensions
{
	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["Jwt:Issuer"],
				ValidAudience = configuration["Jwt:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
			};
		});

		services.AddAuthorization();

		services.AddSingleton<IJwtTokenService, JwtTokenService>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

		return services;
	}
}
