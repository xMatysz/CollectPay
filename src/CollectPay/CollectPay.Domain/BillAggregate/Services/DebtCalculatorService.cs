﻿using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.BillAggregate.Services;
public class DebtCalculatorService
{
	private readonly Dictionary<Guid, decimal> _balance = new();
	private List<Debt> _allDebts = new();

	public List<Debt> Recalculate(IReadOnlyList<Payment> payments)
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
				debt = payment.Amount / (payment.DebtorIds.Count + 1);
				_balance[payment.CreatorId] -= payment.Amount - debt;
				break;

			case false:
				debt = payment.Amount / payment.DebtorIds.Count;
				_balance[payment.CreatorId] -= payment.Amount;
				break;
		}

		foreach (var debtorId in payment.DebtorIds)
		{
			_balance[debtorId] += debt;
		}
	}

	private List<Debt> CalculateDebt()
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

		return _allDebts.ToList();
	}
}