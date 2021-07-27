using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Jwt.Authentication.Entities
{
	public class TokenValidationResult
	{
		public bool IsValid { get; set; }
		public string ErrorMessage { get; set; }
		public IEnumerable<Claim> Claims { get; set; }
	}
}
