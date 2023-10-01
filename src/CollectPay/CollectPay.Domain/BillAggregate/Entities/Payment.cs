using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public class Payment : Entity
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public List<Guid> BuddyIds { get; set; }

    private Payment(decimal amount, string currency, List<Guid> buddyIds)
    {
        Amount = amount;
        Currency = currency;
        BuddyIds = buddyIds;
    }

    public static ErrorOr<Payment> Create(decimal amount, string currency, List<Guid> buddyIds)
    {
        if (currency.Length > 3)
        {
            Error.Validation();
        }

        return new Payment(amount, currency, buddyIds);
    }
}