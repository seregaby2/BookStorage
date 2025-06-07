using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStorage.Infrastructure.Auth
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly string _key;
		private readonly string _issuer;
		private readonly double _tokenLifetimeMinutes;

		public JwtTokenService(IConfiguration config)
		{
			_key = config["Jwt:Key"]!;
			_issuer = config["Jwt:Issuer"]!;
			_tokenLifetimeMinutes = double.Parse(config["Jwt:TokenLifetimeMinutes"]!);

		}

		public string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _issuer,
				audience: _issuer,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_tokenLifetimeMinutes),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
