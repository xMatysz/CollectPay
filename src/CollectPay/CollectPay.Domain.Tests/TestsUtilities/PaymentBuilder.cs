using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Domain.Tests.TestsUtilities;

public class PaymentBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private bool _isCreatorIncluded = false;
	private decimal _amount = 21.37m;
	private string _currency = "PLN";
	private List<Guid> _debtors = new(){Guid.NewGuid()};

	public PaymentBuilder WithCreatorId(Guid id)
	{
		_creatorId = id;
		return this;
	}

	public PaymentBuilder AsCreatorIncluded(bool isCreatorIncluded)
	{
		_isCreatorIncluded = isCreatorIncluded;
		return this;
	}

	public PaymentBuilder WithAmount(decimal amount)
	{
		_amount = amount;
		return this;
	}

	public PaymentBuilder WithCurrency(string currency)
	{
		_currency = currency;
		return this;
	}

	public PaymentBuilder WithDebtors(List<Guid> debtors)
	{
		_debtors = debtors;
		return this;
	}

	public Payment Build()
	{
		var payment = Payment.Create(_creatorId, _isCreatorIncluded, _amount, _currency, _debtors);
		return payment.Value;
	}
}