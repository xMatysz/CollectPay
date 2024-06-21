using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;

public class GetDebtsQueryHandler : IQueryHandler<GetDebtsQuery, Debt[]>
{
	private readonly IBillRepository _billRepository;
	private readonly IDebtService _debtService;

	public GetDebtsQueryHandler(IBillRepository billRepository, IDebtService debtService)
	{
		_billRepository = billRepository;
		_debtService = debtService;
	}

	public async Task<ErrorOr<Debt[]>> Handle(GetDebtsQuery query, CancellationToken cancellationToken)
	{
		var bill = await _billRepository.GetByIdAsync(query.BillId, cancellationToken);

		if (bill is null || !bill.Debtors.Contains(query.BillId))
		{
			return BillErrors.BillNotFound;
		}

		return _debtService.CalculateDebt(bill.Payments);
	}
}