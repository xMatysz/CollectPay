using CollectPay.Application.Common.Repositories;

namespace CollectPay.Application.Tests.Unit;

public class UnitTestBase
{
	protected IBillRepository BillRepository { get; } = Substitute.For<IBillRepository>();
	protected IUserRepository UserRepository { get; } = Substitute.For<IUserRepository>();
}