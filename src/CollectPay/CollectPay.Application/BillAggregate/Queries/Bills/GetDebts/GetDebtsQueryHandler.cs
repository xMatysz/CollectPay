using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
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

	public async Task<ErrorOr<Debt[]>> Handle(GetDebtsQuery request, CancellationToken cancellationToken)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		var payments = bill!.Payments;

		var debts = await _debtService.CalculateDebt(payments);
		return debts.ToArray();
	}
}