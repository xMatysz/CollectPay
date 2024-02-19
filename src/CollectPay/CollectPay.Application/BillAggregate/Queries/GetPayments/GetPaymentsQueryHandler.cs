using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Entities;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Queries.GetPayments;

public class GetPaymentsQueryHandler : IQueryHandler<GetPaymentsQuery, Payment[]>
{
	private readonly IBillRepository _billRepository;

	public GetPaymentsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Payment[]>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null)
		{
			return Error.NotFound();
		}

		return bill.Payments.ToArray();

	}
}