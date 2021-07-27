using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Web.Jwt.Authentication.Services;

namespace Web.Jwt.Authentication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticator _authenticator;
		private readonly IJwtValidator _jwtValidator;

		public AuthenticationController()
		{
			_authenticator = new Authenticator();
			_jwtValidator = new JwtValidator();	
		}


		//key: 7A32A09B-215F-418A-A485-58921261B030
		//token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJUZXN0MDExMTAiLCJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJpc3MiOiJ3ZWF0aGVyLWFzdHJvIn0.EW2Q6jGFnVsPw2ersPo5ch3GdbAkOn8rqfiWycuEHA8
		[HttpGet]
		[Route("validate-token")]
		public IActionResult ValidateToken([FromHeader] string authorization)
		{
			if(string.IsNullOrEmpty(authorization)) return Unauthorized();

			if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				var token = authorization["Bearer ".Length..].Trim();
				var tokenValidationResult = _jwtValidator.ValidateJwtToken(token);

				if (tokenValidationResult.IsValid)
				{
					return Ok();
				}
			}
			return Unauthorized();
		}
	}
}
