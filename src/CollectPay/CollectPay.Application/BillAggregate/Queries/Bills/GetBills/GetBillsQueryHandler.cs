using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Dtos;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public class GetBillsQueryHandler : IQueryHandler<GetBillsQuery, BillDto[]>
{
	private readonly IBillRepository _billRepository;

	public GetBillsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<BillDto[]>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
	{
		var bills = await _billRepository.GetAllForUserAsync(request.UserId, cancellationToken);
		return bills.Select(bill => new BillDto(bill)).ToArray();
	}
}