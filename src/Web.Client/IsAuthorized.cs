using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Web.Client.Contracts;

namespace Web.Client
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class IsAuthorizedAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			IServiceProvider serviceProvider = context.HttpContext.RequestServices;
			var logger = (ILogger<IsAuthorizedAttribute>)serviceProvider.GetService(typeof(ILogger<IsAuthorizedAttribute>));

			//Read token string from request header
			var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].SingleOrDefault();

			if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				string token = authorizationHeader.Substring("Bearer ".Length).Trim();
				var authenticationService = (IAuthenticationService)serviceProvider.GetService(typeof(IAuthenticationService));
				try
				{
					var result = authenticationService.ValidateTokenAsync(token).Result;
					if (!result)
						//context.HttpContext.Items.Add("IsAuthorised", result.IsAuthorised);
						context.Result = new UnauthorizedResult();
					
					return;
				}
				catch (Exception ex)
				{
					logger.LogError("Token seems to be invalid", ex);
				}
			}

			context.Result = new UnauthorizedResult();
		}
	}
}
