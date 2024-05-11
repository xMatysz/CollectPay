using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public class GetBillsQueryHandler : IQueryHandler<GetBillsQuery, Bill[]>
{
	private readonly IBillRepository _billRepository;

	public GetBillsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Bill[]>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
	{
		return await _billRepository.GetAllForUserAsync(request.UserId, cancellationToken);
	}
}