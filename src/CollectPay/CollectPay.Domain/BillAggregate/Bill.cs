using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments = new ();
    private readonly List<Guid> _buddyIds;

    public string Name { get; }

    public IReadOnlyList<Guid> BuddyIds => _buddyIds.AsReadOnly();

    public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();

    public Bill(string name, List<Guid> buddyIds)
    {
        Name = name;
        _buddyIds = buddyIds;
    }

    private void AddPayment(Payment newPayment)
    {
        _payments.Add(newPayment);
    }

    private void DeletePayment(Guid id)
    {
        var itemToRemove = _payments.FirstOrDefault(x => x.Id == id);
        _payments.Remove(itemToRemove);

        if (itemToRemove is null)
        {

        }
    }

    private void AddBuddy(Guid buddyId)
    {
        _buddyIds.Add(buddyId);
    }

    private void RemoveBuddy(Guid buddyId)
    {
        _buddyIds.Remove(buddyId);
    }
}