using CollectPay.Application.BillAggregate.Queries.GetBills;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Api.ApiTests.Controllers.BillControllerTests.Doubles;

public class SuccessfulGetBillHandler : IRequestHandler<GetBillsQuery, List<Bill>>
{
	public Task<List<Bill>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
	{
		return Task.FromResult(new List<Bill>());
	}
}