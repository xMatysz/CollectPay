using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Infrastructure.Services;

public class DebtService : IDebtService
{
	private readonly Dictionary<Guid, decimal> _balance = new();
	private readonly List<Debt> _allDebts = new();

	public Debt[] CalculateDebt(IReadOnlyCollection<Payment> payments)
	{
		var allUsers = payments
			.SelectMany(x=>x.Debtors)
			.Concat(payments.Select(x=>x.CreatorId))
			.Distinct()
			.ToList();

		Initialization(allUsers);

		foreach (var payment in payments)
		{
			AssignDebt(payment);
		}

		return CalculateDebt();
	}

	private void Initialization(List<Guid> allUsers)
	{
		foreach (var user in allUsers)
		{
			_balance.TryAdd(user, decimal.Zero);
		}
	}

	private void AssignDebt(Payment payment)
	{
		var debt = payment.Amount.Value / payment.Debtors.Count;
		_balance[payment.CreatorId] -= payment.Amount.Value;

		foreach (var debtorId in payment.Debtors)
		{
			_balance[debtorId] += debt;
		}
	}

	private Debt[] CalculateDebt()
	{
		var sortedPayers = _balance
			.Where(x => x.Value < 0)
			.OrderBy(x => x.Value)
			.Select(x => x.Key)
			.ToArray();

		var sortedDebtors = _balance
			.Where(x => x.Value > 0)
			.OrderByDescending(x => x.Value)
			.Select(x => x.Key)
			.ToArray();

		var payerIndex = 0;
		var debtorIndex = 0;

		while (payerIndex < sortedPayers.Length && debtorIndex < sortedDebtors.Length)
		{
			var payer = sortedPayers[payerIndex];
			var debtor = sortedDebtors[debtorIndex];

			var payerBalance = _balance[payer];
			var debtorBalance = _balance[debtor];

			var minTransfer = Math.Min(-payerBalance, debtorBalance);

			if (minTransfer > 0)
			{
				_balance[payer] += minTransfer;
				_balance[debtor] -= minTransfer;
				_allDebts.Add(Debt.Create(debtor, minTransfer, payer));
			}

			if (_balance[payer] > -0.01M)
			{
				payerIndex++;
			}

			if (_balance[debtor] < 0.01M)
			{
				debtorIndex++;
			}
		}

		return _allDebts.ToArray();
	}
}