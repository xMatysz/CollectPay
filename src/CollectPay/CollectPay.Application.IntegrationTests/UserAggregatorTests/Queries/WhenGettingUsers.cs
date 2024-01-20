using CollectPay.Application.UserAggregate.Queries.GetUserQuery;

namespace CollectPay.Application.IntegrationTests.UserAggregatorTests.Queries;

public class WhenGettingUsers : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly GetUserQueryHandler _handler;

	public WhenGettingUsers(WebApiFactory factory)
		: base(factory)
	{
		_handler = new GetUserQueryHandler(UserRepository);
	}

	[Fact]
	public async Task ShouldGetUserById()
	{
		var user = new UserBuilder().Build();
		await AddEntityToDbAsync(user);
		var query = new GetUserQuery(user.Id);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.IsError.Should().BeFalse();

		var userFromDb = result.Value;
		userFromDb.Should().BeEquivalentTo(user);
	}
}