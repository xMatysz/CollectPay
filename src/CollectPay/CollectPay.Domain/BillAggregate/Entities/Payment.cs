using System.ComponentModel.DataAnnotations.Schema;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	public Guid CreatorId { get; set; }

    public bool IsCreatorIncluded { get; set; }

    public Amount Amount { get; set; }

    [NotMapped]
    public IEnumerable<Guid> DebtorIds { get; set; }

    private Payment(Guid creatorId, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    Id = Guid.NewGuid();
	    CreatorId = creatorId;
	    IsCreatorIncluded = isCreatorIncluded;
	    Amount = amount;
	    DebtorIds = debtorIds;
    }

    public static ErrorOr<Payment> Create(Guid creator, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    var ids = debtorIds.ToArray();

	    if (ids.Contains(creator))
	    {
		    return PaymentErrors.CreatorCannotBeDebtor;
	    }

        return new Payment(creator, isCreatorIncluded, amount, ids);
    }

    private Payment()
    {
    }
}