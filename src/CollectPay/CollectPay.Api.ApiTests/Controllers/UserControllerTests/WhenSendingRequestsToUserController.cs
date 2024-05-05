﻿using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.User;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.UserAggregate.Login;
using CollectPay.Application.UserAggregate.Register;
using CollectPay.Domain.UserAggregate;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.UserControllerTests;

public class WhenSendingRequestsToUserController : ControllerTestBase
{
	[Fact]
	public async Task ShouldRegisterUser()
	{
		const string url = UserRoutes.Register;
		const string email = "test@email.com";
		const string password = "testPassword";
		var request = new RegisterUserRequest(email, password);
		ConfigureHandler<RegisterUserCommand, ErrorOr<User>, SuccessFullHandler<RegisterUserCommand, ErrorOr<User>>>();

		var result = await Client.PostAsJsonAsync(url, request);

		result.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task Should_LoginUser()
	{
		const string url = UserRoutes.Login;
		const string email = "test@email.com";
		const string password = "testPassword";
		var request = new LoginUserRequest(email, password);
		ConfigureHandler<LoginUserQuery, ErrorOr<string>, SuccessFullHandler<LoginUserQuery, ErrorOr<string>>>();

		var result = await Client.PostAsJsonAsync(url, request);

		result.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);
	}
}