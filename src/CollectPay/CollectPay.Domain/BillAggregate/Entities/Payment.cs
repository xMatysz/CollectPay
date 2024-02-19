using System.ComponentModel.DataAnnotations.Schema;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	public Guid CreatorId { get; init; }

    public bool IsCreatorIncluded { get; init; }

    public Amount Amount { get; init; }

    public Guid BillId { get; init; }

    [NotMapped]
    public IEnumerable<Guid> DebtorIds { get; init; }


    private Payment(Guid billId, Guid creatorId, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    Id = Guid.NewGuid();
	    BillId = billId;
	    CreatorId = creatorId;
	    IsCreatorIncluded = isCreatorIncluded;
	    Amount = amount;
	    DebtorIds = debtorIds;
    }

    public static ErrorOr<Payment> Create(Guid billId, Guid creator, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    var ids = debtorIds.ToArray();

	    if (ids.Contains(creator))
	    {
		    return PaymentErrors.CreatorCannotBeDebtor;
	    }

        return new Payment(billId, creator, isCreatorIncluded, amount, ids);
    }

    private Payment()
    {
    }
}