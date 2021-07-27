using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Jwt.Authentication.Entities
{
	public class AuthenticationResult
	{
		public AuthenticationResult()
		{
			IsAuthenticated = false;
		}
		public bool IsAuthenticated { get; set; }
		public string JwtToken { get; set; }
	}
}
