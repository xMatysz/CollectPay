using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Domain.BillAggregate.Services;
public class DebtCalculatorService
{
	private readonly Dictionary<Guid, decimal> _balance = new();

	public void Recalculate(IReadOnlyList<Payment> payments)
	{
		foreach (var payment in payments)
		{
			Initialization(payment.Creator, payment.BuddyIds);

			AssignDebt(payment);
		}
	}

	private void AssignDebt(Payment payment)
	{
		decimal debt;

		switch (payment.IsCreatorIncluded)
		{
			case true:
				debt = payment.Amount / (payment.BuddyIds.Count + 1);
				_balance[payment.Creator] -= payment.Amount + debt;
				break;
			case false:
				debt = payment.Amount / payment.BuddyIds.Count;
				_balance[payment.Creator] -= payment.Amount;
				break;
		}

		foreach (var buddyId in payment.BuddyIds)
		{
			_balance[buddyId] += debt;
		}
	}

	private void Initialization(Guid paymentCreator, List<Guid> paymentBuddyIds)
	{
		_balance.TryAdd(paymentCreator, decimal.Zero);
		foreach (var buddyId in paymentBuddyIds)
		{
			_balance.TryAdd(buddyId, decimal.Zero);
		}
	}
}