using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

using TokenValidationResult = Web.Jwt.Authentication.Entities.TokenValidationResult;

namespace Web.Jwt.Authentication.Services
{
	public class JwtValidator : IJwtValidator
	{
		private JwtSecurityToken _jwtSecurityToken;
		private Claim _iss;
		private Claim _jti;
		public TokenValidationResult ValidateJwtToken(string jwtToken)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				new JwtSecurityToken(jwtToken);
			}
			catch (Exception ex)
			{
				return new TokenValidationResult
				{
					IsValid = false,
					ErrorMessage = $"Error occurred while reading jwt token: {ex.Message}"
				};
			}

			_jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: jwtToken);
			_jti = _jwtSecurityToken.Claims.SingleOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Jti));
			if (_jti is null) return new TokenValidationResult { IsValid = false, ErrorMessage = "Token identifier missing" };

			_iss = _jwtSecurityToken.Claims.SingleOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Iss));
			if (_iss is null) return new TokenValidationResult { IsValid = false, ErrorMessage = "Token issuer missing" };

			SymmetricSecurityKey securityKey = CreateSecurityKey("7A32A09B-215F-418A-A485-58921261B030"); //Read Specific to token using iss or jti

			var validationParameters = ValidationCriteria(securityKey);

			try
			{
				new JwtSecurityTokenHandler()
					.ValidateToken(jwtToken, validationParameters, out var securityToken);

				return new TokenValidationResult { IsValid = true, Claims = _jwtSecurityToken.Claims };
			}
			catch (SecurityTokenValidationException ex)
			{
				return new TokenValidationResult { IsValid = false, ErrorMessage = $"Error in token:{ex.Message}" };
			}
			catch (Exception ex)
			{
				return new TokenValidationResult { IsValid = false, ErrorMessage = $"Error in token:{ex.Message}" };
			}
		}

		private TokenValidationParameters ValidationCriteria(SymmetricSecurityKey securityKey)
		{
			return new TokenValidationParameters
			{
				//toggle the parameters you want to validate against token
				IssuerSigningKey = securityKey,
				RequireSignedTokens = true,
				ValidateIssuer = true,
				RequireExpirationTime = false,
				ValidateLifetime = false,
				ValidateAudience = false,
				ValidIssuers = new List<string> { "accu-weather", "weather-astro" }
			};
		}

		private SymmetricSecurityKey CreateSecurityKey(string key)
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		}
	}
}
