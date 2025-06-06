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
		private readonly IUserRepository _userRepository;
		private readonly IJwtTokenService _jwt;
		private readonly IPasswordHasher<User> _passwordHasher;

		public AuthController(IUserRepository userRepository, IJwtTokenService jwt, IPasswordHasher<User> passwordHasher)
		{
			_userRepository = userRepository;
			_jwt = jwt;
			_passwordHasher = passwordHasher;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var existing = await _userRepository.GetByEmailAsync(registerDto.Email);
			if (existing != null)
				return BadRequest("User already exists.");

			var user = new User { Id = Guid.NewGuid(), Email = registerDto.Email, Role = registerDto.Role };
			user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

			await _userRepository.CreateAsync(user);
			return Ok("User registered");
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await _userRepository.GetByEmailAsync(loginDto.Email);
			if (user == null)
				return Unauthorized("Invalid credentials");

			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
			if (result == PasswordVerificationResult.Failed)
				return Unauthorized("Invalid credentials");

			var token = _jwt.GenerateToken(user);
			return Ok(new JwtTokenResponse { Token = token });
		}
	}
}
