using System.Threading.Tasks;

using Web.Client.Entities;

namespace Web.Client.Contracts
{
	public interface IAuthenticationService
	{
		//Task<AuthenticationApiResponse> ValidateTokenAsync(string token);

		Task<bool> ValidateTokenAsync(string token);
	}
}
