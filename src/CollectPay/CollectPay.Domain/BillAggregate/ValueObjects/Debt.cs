using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate.ValueObjects;

public class Debt : ValueObject
{
	public Guid Debtor { get; }
	public decimal DebtAmount { get; }
	public Guid Creditor { get; }

	private Debt(Guid debtor, decimal debtAmount, Guid creditor)
	{
		Debtor = debtor;
		DebtAmount = debtAmount;
		Creditor = creditor;
	}

	public static Debt Create(Guid debtor, decimal debtAmount, Guid creditor)
	{
		var fixedAmount = Math.Round(debtAmount, 2);
		return new Debt(debtor, fixedAmount, creditor);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Debtor;
		yield return DebtAmount;
		yield return Creditor;
	}
}