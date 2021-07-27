﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Web.Jwt.Authentication.Entities;

namespace Web.Jwt.Authentication.Services
{
	public interface IJwtValidator
	{
		TokenValidationResult ValidateJwtToken(string token);
	}
}
