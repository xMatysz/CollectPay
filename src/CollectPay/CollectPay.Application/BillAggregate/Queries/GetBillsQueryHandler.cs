using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using MediatR;

namespace CollectPay.Application.BillAggregate.Queries;

public class GetBillsQueryHandler : IRequestHandler<GetBillsQuery, List<Bill>>
{
	private readonly IBillRepository _billRepository;

	public GetBillsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public Task<List<Bill>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
	{
		return _billRepository.GetAllAsync(cancellationToken);
	}
}