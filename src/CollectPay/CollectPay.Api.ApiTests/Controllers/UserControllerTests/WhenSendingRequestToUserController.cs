using System.Net.Http.Json;
using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Controllers.BillControllerTests.Doubles;
using CollectPay.Application.UserAggregate.Commands.CreateUser;
using CollectPay.Tests.Shared.Builders;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.UserControllerTests;

public class WhenSendingRequestToUserController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnNewUserId()
	{
		const string url = UserRoutes.Create;
		var request = new CreateUserRequest(UserBuilder.TestEmail, UserBuilder.TestPassword, UserBuilder.TestNickName);
		ConfigureHandler<CreateUserCommand, ErrorOr<CreateUserCommandResult>, SuccessfulHandler<CreateUserCommand,ErrorOr<CreateUserCommandResult>>>();

		var response = await Client.PostAsJsonAsync(url, request);
		var content = await response.Content.ReadFromJsonAsync<CreateUserResponse>();

		content.Should().NotBeNull();
	}
}