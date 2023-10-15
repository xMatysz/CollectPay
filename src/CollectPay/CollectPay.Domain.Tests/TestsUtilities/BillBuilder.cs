using CollectPay.Domain.BillAggregate;

namespace CollectPay.Domain.Tests.TestsUtilities;

public class BillBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private string _billName = "TestBill";
	private List<Guid> _buddyIds = new();

	public BillBuilder WithCreatorId(Guid id)
	{
		_creatorId = id;
		return this;
	}

	public BillBuilder WithName(string name)
	{
		_billName = name;
		return this;
	}

	public BillBuilder WithBuddyIds(List<Guid> buddyIds)
	{
		_buddyIds = buddyIds;
		return this;
	}

	public Bill Build()
	{
		return new Bill(_creatorId, _billName, _buddyIds);
	}
}