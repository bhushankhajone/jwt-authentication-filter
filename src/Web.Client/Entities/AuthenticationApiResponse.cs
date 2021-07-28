using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Client.Entities
{
	public class AuthenticationApiResponse
	{
		public bool IsAuthorised { get; set; }
		public string Context { get; set; }
	}
}
