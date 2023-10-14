using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	public Guid CreatorId { get; set; }
    public bool IsCreatorIncluded { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public List<Guid> DebtorIds { get; set; }

    private Payment(Guid creatorId, bool isCreatorIncluded, decimal amount, string currency, List<Guid> debtorIds)
    {
	    CreatorId = creatorId;
	    IsCreatorIncluded = isCreatorIncluded;
	    Amount = amount;
	    Currency = currency;
	    DebtorIds = debtorIds;
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