using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    // private readonly List<Guid> _buddyIds;
    private readonly List<Payment> _payments = new();

    public Guid CreatorId { get; init; }
    public string Name { get; init; }
    // public IReadOnlyList<Guid> BuddyIds => _buddyIds.AsReadOnly();
    public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();

    public Bill(Guid creatorId, string name, List<Guid> buddyIds)
    {
	    CreatorId = creatorId;
        Name = name;
        // _buddyIds = buddyIds;
    }

    public void AddBuddy(Guid buddyId)
    {
	   // _buddyIds.Add(buddyId);
    }

    public void RemoveBuddy(Guid buddyId)
    {
	    // _buddyIds.Remove(buddyId);
    }

    public void AddPayment(Payment newPayment)
    {
        _payments.Add(newPayment);
    }

    public void DeletePayment(Guid id)
    {
        var itemToRemove = _payments.FirstOrDefault(x => x.Id == id);
        _payments.Remove(itemToRemove);
    }

    private Bill()
    {
    }
}