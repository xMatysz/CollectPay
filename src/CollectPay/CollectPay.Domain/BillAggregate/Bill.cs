using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Services;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments;
    private readonly List<Guid> _buddyIds;
    private readonly DebtCalculatorService _debtCalculator = new();
    private List<Debt> _debts;

    public Guid CreatorId { get; }
    public string Name { get; }
    public IReadOnlyList<Guid> BuddyIds => _buddyIds.AsReadOnly();
    public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();
    public IReadOnlyCollection<Debt> Debts => _debts.AsReadOnly();

    public Bill(Guid creatorId, string name, List<Guid> buddyIds)
    {
	    CreatorId = creatorId;
        Name = name;
        _buddyIds = buddyIds;

        _payments = new List<Payment>();
    }

    public void AddBuddy(Guid buddyId)
    {
	    _buddyIds.Add(buddyId);
    }

    public void RemoveBuddy(Guid buddyId)
    {
	    _buddyIds.Remove(buddyId);
    }

    public void AddPayment(Payment newPayment)
    {
        _payments.Add(newPayment);

        CalculateDebts();
    }

    public void DeletePayment(Guid id)
    {
        var itemToRemove = _payments.FirstOrDefault(x => x.Id == id);
        _payments.Remove(itemToRemove);

        CalculateDebts();
    }

    private void CalculateDebts()
    {
	    _debts = _debtCalculator.Recalculate(Payments);
    }
}