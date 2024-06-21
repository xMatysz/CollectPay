using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Tests.Shared.Builders;

public class PaymentBuilder : TestBuilder<Payment>
{
	private Guid _id = Guid.NewGuid();
	private Guid _billId = Guid.NewGuid();
	private Guid _creatorId = Guid.NewGuid();
	private Amount _amount = DefaultAmount;
	private string _name = DefaultName;
	private IEnumerable<Guid> _debtors = [];

	public static string DefaultName => "Test Payment";
	public static Amount DefaultAmount { get; } = Amount.Create(100, "USD").Value;

	public static PaymentBuilder Default()
	{
		return new PaymentBuilder();
	}

	public static PaymentBuilder Like(Payment payment)
	{
		return new PaymentBuilder()
			.WithId(payment.Id)
			.WithBillId(payment.BillId)
			.WithName(payment.Name)
			.WithCreatorId(payment.CreatorId)
			.WithAmount(payment.Amount)
			.WithDebtors(payment.Debtors);
	}

	public PaymentBuilder WithId(Guid id)
	{
		_id = id;
		return this;
	}

	public PaymentBuilder WithBillId(Guid billId)
	{
		_billId = billId;
		return this;
	}

	public PaymentBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public PaymentBuilder WithCreatorId(Guid creatorId)
	{
		_creatorId = creatorId;
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

	public override Payment Build()
	{
		var payment = new Payment(_billId, _name, _creatorId, _amount, _debtors);
		OverrideProperty(nameof(Payment.Id), payment, _id);
		return payment;
	}
}