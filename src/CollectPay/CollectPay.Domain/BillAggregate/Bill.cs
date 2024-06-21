using CollectPay.Core.Extensions;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments = [];
    private readonly List<Guid> _debtors = [];

    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    // fix for IReadOnlyCollection in ef core 9
    public IList<Guid> Debtors => _debtors.AsReadOnly();

    public Guid CreatorId { get; init; }
    public string Name { get; set; }

    public Bill(Guid creatorId, string name)
    {
	    Id = Guid.NewGuid();
	    CreatorId = creatorId;
        Name = name;

        _debtors.Add(creatorId);
    }

    public ErrorOr<Updated> AddPayment(Payment newPayment)
    {
	    if (Payments.Any(x => Equals(x, newPayment)))
	    {
		    return BillErrors.PaymentAlreadyExist;
	    }

	    _payments.Add(newPayment);
	    return Result.Updated;
    }

    public ErrorOr<Deleted> RemovePayment(Guid id)
    {
	    var itemToRemove = _payments.FirstOrDefault(x => x.Id == id);

	    if (itemToRemove is null)
	    {
		    return BillErrors.PaymentNotExist;
	    }

	    _payments.Remove(itemToRemove);

	    return Result.Deleted;
    }

    public ErrorOr<Updated> Update(string name, Guid[] userIdsToAdd, Guid[] userIdsToRemove)
    {
	    var errors = new List<Error>();

	    var updateName = UpdateName(name);
	    if (updateName.IsError)
	    {
		    errors.AddRange(updateName.Errors);
	    }

	    var addDebtors = AddDebtors(userIdsToAdd);
	    if (addDebtors.IsError)
	    {
		    errors.AddRange(addDebtors.Errors);
	    }

	    var removeDebtors = RemoveDebtors(userIdsToRemove);
	    if (removeDebtors.IsError)
	    {
		    errors.AddRange(removeDebtors.Errors);
	    }

	    return errors.Any() ? errors : Result.Updated;
    }

    private ErrorOr<Success> UpdateName(string name)
    {
	    if (name.IsNullEmptyOrWhitespace())
	    {
		    return BillErrors.NameCannotBeEmpty;
	    }

	    Name = name;
	    return Result.Success;
    }

    private ErrorOr<Success> AddDebtors(Guid[] userIdsToAdd)
    {
	    var existingDebtors = Debtors.Where(userIdsToAdd.Contains).ToArray();
	    if (existingDebtors.Any())
	    {
		    return existingDebtors.Select(BillErrors.DebtorIsAlreadyAdded).ToArray();
	    }

	    _debtors.AddRange(userIdsToAdd);
	    return Result.Success;
    }

    private ErrorOr<Success> RemoveDebtors(Guid[] userIdsToRemove)
    {
	    if (userIdsToRemove.Contains(CreatorId))
	    {
		    return BillErrors.CannotRemoveCreatorFromDebtors;
	    }

	    var notExistingDebtors = userIdsToRemove.Where(id => !Debtors.Contains(id)).ToArray();
	    if (notExistingDebtors.Any())
	    {
		    return notExistingDebtors.Select(BillErrors.DebtorNotFound).ToArray();
	    }

	    _debtors.RemoveAll(userIdsToRemove.Contains);
	    return Result.Success;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Bill()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}