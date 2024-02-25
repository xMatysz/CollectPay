using CollectPay.Application.Common.Repositories;

namespace CollectPay.Application.UnitTests;

public class UnitTestBase
{
	protected IBillRepository BillRepository { get; } = Substitute.For<IBillRepository>();
}