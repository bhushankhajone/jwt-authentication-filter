using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Web.Jwt.Authentication.Entities;

namespace Web.Jwt.Authentication.Services
{
	public class Authenticator:IAuthenticator
	{
		public AuthenticationResult AuthenticateRequester(string token)
		{
			throw new NotImplementedException();
		}
	}
}
