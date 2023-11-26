using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Tests.Shared.Builders;

public class PaymentBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private bool _isCreatorIncluded = false;
	private Amount _amount = new(21.37m ,"PLN");
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

	public PaymentBuilder WithAmount(Amount amount)
	{
		_amount = amount;
		return this;
	}

	public PaymentBuilder WithDebtors(IEnumerable<Guid> debtors)
	{
		_debtors = debtors;
		return this;
	}

	public Payment Build()
	{
		var payment = Payment.Create(_creatorId, _isCreatorIncluded, _amount, _debtors);
		return payment.Value;
	}
}