using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	public Guid Creator { get; set; }
    public bool IsCreatorIncluded { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public List<Guid> BuddyIds { get; set; }

    private Payment(Guid creator, bool isCreatorIncluded, decimal amount, string currency, List<Guid> buddyIds)
    {
	    Creator = creator;
	    IsCreatorIncluded = isCreatorIncluded;
	    Amount = amount;
	    Currency = currency;
	    BuddyIds = buddyIds;
    }

    public static ErrorOr<Payment> Create(Guid creator, bool isCreatorIncluded, decimal amount, string currency, List<Guid> buddyIds)
    {
        if (currency.Length > 3)
        {
            Error.Validation();
        }

        return new Payment(creator, isCreatorIncluded, amount, currency, buddyIds);
    }
}