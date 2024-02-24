using System.Net.Http.Json;
using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Pages.Register;

namespace CollectionPay.Maui.Services;

public class UserService : ServiceBase
{
	public UserService(HttpClient client)
		: base(client)
	{
	}

	public Task SendCreateUserRequest(RegisterModel model, CancellationToken cancellationToken = default)
	{
		var request = new CreateUserRequest(model.Email, model.Password, model.NickName);
		return HttpClient.PostAsJsonAsync(UserRoutes.Create, request, cancellationToken);
	}
}