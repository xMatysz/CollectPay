using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.Services;

public interface IDebtService
{
	Debt[] CalculateDebt(IReadOnlyCollection<Payment> payments);
}