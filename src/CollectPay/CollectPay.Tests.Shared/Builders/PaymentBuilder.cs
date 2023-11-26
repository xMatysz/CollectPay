using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Tests.Shared.Builders;

public class PaymentBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private bool _isCreatorIncluded = false;
	private decimal _amount = 21.37m;
	private string _currency = "PLN";
	private IEnumerable<Guid> _debtors = new[] { Guid.NewGuid() };

	public PaymentBuilder WithCreatorId(Guid creatorId)
	{
		_creatorId = creatorId;
		return this;
	}

	public PaymentBuilder WithCreatorIncluded()
	{
		_isCreatorIncluded = true;
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

	public PaymentBuilder WithDebtors(IEnumerable<Guid> debtors)
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