namespace CollectPay.Application.IntegrationTests.MappingTests;

public class WhenQueryingUserEntities : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	public WhenQueryingUserEntities(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldGetUserFromDb()
	{
		var user = new UserBuilder().Build();

		await AddEntityToDbAsync(user);

		var userFromDb = await UserRepository.GetByIdAsync(user.Id, CancellationToken.None);
		userFromDb.Should().BeEquivalentTo(user);
	}
}