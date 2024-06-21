using CollectPay.Domain.BillAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class BillBuilder : TestBuilder<Bill>
{
	private Guid _creatorId = DefaultCreatorId;
	private string _name = DefaultName;

	public static Guid DefaultCreatorId { get; } = Guid.NewGuid();
	public static string DefaultName => "Test Bill";

	public static BillBuilder Default()
	{
		return new BillBuilder();
	}

	public  BillBuilder WithCreatorId(Guid creatorId)
	{
		_creatorId = creatorId;
		return this;
	}

	public BillBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public override Bill Build()
	{
		return new Bill(_creatorId, _name);
	}

	public Bill BuildWithDebtors(Guid[] debtorIds)
	{
		var bill = Build();
		bill.Update(bill.Name, debtorIds, []);
		return bill;
	}
}