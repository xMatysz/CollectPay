using CollectPay.Domain.BillAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class BillBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private string _billName = "TestBill";

	public BillBuilder WithCreatorId(Guid creatorId)
	{
		_creatorId = creatorId;
		return this;
	}

	public BillBuilder WithName(string name)
	{
		_billName = name;
		return this;
	}

	public Bill Build()
	{
		return new Bill(_creatorId, _billName);
	}
}