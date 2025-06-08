using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using BookStorage.WebApi.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IJwtTokenService _jwt;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IUserService _userService;
		private readonly ILogger<AuthController> _logger;

		public AuthController(IJwtTokenService jwt, IPasswordHasher<User> passwordHasher, IUserService userSerive, ILogger<AuthController> logger)
		{
			_jwt = jwt;
			_passwordHasher = passwordHasher;
			_userService = userSerive;
			_logger = logger;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var existing = await _userService.GetByEmailAsync(registerDto.Email);
			if (existing != null)
				return BadRequest("User already exists.");

			var user = new User { Id = Guid.NewGuid(), Email = registerDto.Email, Role = registerDto.Role };
			user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

			await _userService.CreateAsync(user);
			return Ok("User registered");
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
		{
			_logger.LogInformation("User attempting login with email: {Email}", loginDto.Email);

			var user = await _userService.GetByEmailAsync(loginDto.Email);
			if (user == null)
			{
				_logger.LogWarning("Login failed for {Email}", loginDto.Email);

				return Unauthorized("Invalid credentials");
			}


			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
			if (result == PasswordVerificationResult.Failed)
			{
				_logger.LogWarning("Login failed for {Email}", loginDto.Email);

				return Unauthorized("Invalid credentials");
			}

			var token = _jwt.GenerateToken(user);

			_logger.LogInformation("User logged in successfully: {Email}", loginDto.Email);

			return Ok(new JwtTokenResponse { Token = token });
		}
	}
}
