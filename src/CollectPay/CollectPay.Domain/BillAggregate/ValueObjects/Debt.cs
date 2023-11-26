using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate.ValueObjects;

public class Debt : ValueObject
{
	public Guid Debtor { get; }
	public decimal DebtAmount { get; }
	public Guid Creditor { get; }

	public Debt(Guid debtor, decimal debtAmount, Guid creditor)
	{
		Debtor = debtor;
		DebtAmount = debtAmount;
		Creditor = creditor;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Debtor;
		yield return DebtAmount;
		yield return Creditor;
	}
}