using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private List<Payment> _payments;

    public string Name { get; }
    public List<Guid> BuddyIds { get; }

    public Bill(string name, List<Guid> buddyIds)
    {
        Name = name;
        BuddyIds = buddyIds;
    }

    private void AddPayment(Payment newPayment)
    {
        _payments.Add(newPayment);
    }

    private void DeletePayment(Guid id)
    {
        var itemToRemove = _payments.First(x => x.Id == id);
        _payments.Remove(itemToRemove);
    }
}