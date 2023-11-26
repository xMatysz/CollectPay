using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Infrastructure.Services;

public class DebtService : IDebtService
{
	private readonly Dictionary<Guid, decimal> _balance = new();
	private readonly List<Debt> _allDebts = new();

	public Task<List<Debt>> CalculateDebt(IReadOnlyList<Payment> payments)
	{
		var usersFromPayments = payments.Select(x => x.CreatorId).ToList();
		usersFromPayments.AddRange(payments.SelectMany(x => x.DebtorIds));

		var allUsers = usersFromPayments.Distinct().ToList();

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
		decimal debt;

		switch (payment.IsCreatorIncluded)
		{
			case true:
				debt = payment.Amount.Value / (payment.DebtorIds.Count() + 1);
				_balance[payment.CreatorId] -= payment.Amount.Value - debt;
				break;

			case false:
				debt = payment.Amount.Value / payment.DebtorIds.Count();
				_balance[payment.CreatorId] -= payment.Amount.Value;
				break;
		}

		foreach (var debtorId in payment.DebtorIds)
		{
			_balance[debtorId] += debt;
		}
	}

	private Task<List<Debt>> CalculateDebt()
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
				_allDebts.Add(new Debt(debtor, minTransfer, payer));
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

		return Task.FromResult(_allDebts.ToList());
	}
}