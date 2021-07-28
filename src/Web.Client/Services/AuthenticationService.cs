using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Web.Client.Contracts;
using Web.Client.Entities;

namespace Web.Client.Services
{
	public class AuthenticationService: IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		public AuthenticationService()
		{
			_httpClient = new HttpClient();
		}
		//public async Task<AuthenticationApiResponse> ValidateTokenAsync(string token)
		//{
		//	_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);

		//	HttpResponseMessage responseMessage = await _httpClient.GetAsync("");

		//	if (responseMessage.IsSuccessStatusCode)
		//	{
		//		string result = await responseMessage.Content.ReadAsStringAsync();

		//		AuthenticationApiResponse response = JsonConvert.DeserializeObject<AuthenticationApiResponse>(result);

		//		return response;
		//	}
		//	else if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized)
		//	{
		//		throw new AuthenticationException("Request coming from unknown source!");
		//	}
		//	else
		//	{
		//		throw new AuthenticationException("Error occurred while validating request source!");
		//	}
		//}
		
		public async Task<bool> ValidateTokenAsync(string token)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);

			HttpResponseMessage responseMessage = await _httpClient.GetAsync("https://localhost:5001/api/Authentication/validate-token");

			if (responseMessage.IsSuccessStatusCode)
			{
				return true;
				//string result = await responseMessage.Content.ReadAsStringAsync();

				//AuthenticationApiResponse response = JsonConvert.DeserializeObject<AuthenticationApiResponse>(result);
			}
			else if (responseMessage.StatusCode is System.Net.HttpStatusCode.Unauthorized)
			{
				throw new AuthenticationException("Request coming from unknown source!");
			}
			else
			{
				throw new AuthenticationException("Error occurred while validating request source!");
			}
		}
	}
}
