using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;
using ErrorOr;

namespace CollectPay.Tests.Shared.Builders;

public class PaymentBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private bool _isCreatorIncluded = false;
	private Amount _amount = Amount.Create(21.37m ,"PLN").Value;
	private IEnumerable<Guid> _debtors = new[] { Guid.NewGuid() };
	private Guid _billId = Guid.NewGuid();

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

	public PaymentBuilder WithAmount(ErrorOr<Amount> amount)
	{
		_amount = amount.Value;
		return this;
	}

	public PaymentBuilder WithDebtors(params Guid[] debtors)
	{
		_debtors = debtors;
		return this;
	}

	public PaymentBuilder WithBillId(Guid billId)
	{
		_billId = billId;
		return this;
	}

	public Payment Build()
	{
		var payment = Payment.Create(_billId, _creatorId, _isCreatorIncluded, _amount, _debtors);
		return payment.Value;
	}
}