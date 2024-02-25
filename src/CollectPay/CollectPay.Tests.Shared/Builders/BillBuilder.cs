using CollectPay.Domain.BillAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class BillBuilder
{
	private Guid _creatorId = Guid.NewGuid();
	private static string _billName = "TestBill";

	public static readonly string TestName = _billName;

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