using System.ComponentModel.DataAnnotations.Schema;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	public Guid CreatorId { get; private set; }

    public string Name { get; set; }

    public bool IsCreatorIncluded { get; private set; }

    public Amount Amount { get; private set; }

    public Guid BillId { get; private set; }

    [NotMapped]
    public IEnumerable<Guid> DebtorIds { get; private set; }



    private Payment(Guid billId, string name, Guid creatorId, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    Id = Guid.NewGuid();
	    Name = name;
	    BillId = billId;
	    CreatorId = creatorId;
	    IsCreatorIncluded = isCreatorIncluded;
	    Amount = amount;
	    DebtorIds = debtorIds;
    }

    public static ErrorOr<Payment> Create(Guid billId, string name, Guid creator, bool isCreatorIncluded, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    var ids = debtorIds.ToArray();
	    var result = ValidateCreatorAndDebtors(creator, ids);

	    if (result.IsError)
	    {
		    return result.Errors;
	    }

        return new Payment(billId, name, creator, isCreatorIncluded, amount, ids);
    }

    public ErrorOr<Updated> Update(Guid? creatorId,
	    bool? isCreatorIncluded,
	    Amount? amount,
	    Guid[]? debtors)
    {
	    if (creatorId is not null)
	    {
		    CreatorId = creatorId.Value;
	    }

	    if (isCreatorIncluded is not null)
	    {
		    IsCreatorIncluded = isCreatorIncluded.Value;
	    }

	    if (amount is not null)
	    {
		    Amount = amount;
	    }

	    if (debtors is not null)
	    {
		    var canUpdate = ValidateCreatorAndDebtors(CreatorId, debtors);
		    if (canUpdate.IsError)
		    {
			    return canUpdate.Errors;
		    }

		    DebtorIds = debtors;
	    }

	    return Result.Updated;
    }

    private static ErrorOr<Success> ValidateCreatorAndDebtors(Guid creatorId, Guid[] debtors)
    {
	    if (debtors.Contains(creatorId))
	    {
		    return PaymentErrors.CreatorCannotBeDebtor;
	    }

	    return Result.Success;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Payment()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
