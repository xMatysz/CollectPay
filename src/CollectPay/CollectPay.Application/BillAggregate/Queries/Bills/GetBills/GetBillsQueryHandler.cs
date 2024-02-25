using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using ErrorOr;
using MediatR;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, ErrorOr<List<Bill>>>
{
	private readonly IBillRepository _billRepository;

	public GetBillsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<List<Bill>>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
	{
			return await _billRepository.GetAllAsync(cancellationToken);
	}
}