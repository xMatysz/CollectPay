using CollectPay.Core.Extensions;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Entities;

public sealed class Payment : Entity
{
	private readonly List<Guid> _debtors = [];

	public Guid CreatorId { get; private set; }

    public string Name { get; set; }

    public Amount Amount { get; private set; }

    public Guid BillId { get; private set; }

    public IList<Guid> Debtors => _debtors.AsReadOnly();

    public Payment(Guid billId, string name, Guid creatorId, Amount amount, IEnumerable<Guid> debtorIds)
    {
	    Id = Guid.NewGuid();
	    Name = name;
	    BillId = billId;
	    CreatorId = creatorId;
	    Amount = amount;

	    foreach (var ids in debtorIds)
	    {
		    _debtors.Add(ids);
	    }
    }

    public ErrorOr<Updated> Update(string name, Amount amount, Guid[] userIdsToAdd, Guid[] userIdsToRemove)
    {
	    var errors = new List<Error>();

	    var updateName = UpdateName(name);
	    if (updateName.IsError)
	    {
		    errors.AddRange(updateName.Errors);
	    }

	    var updateAmount = UpdateAmount(amount);
	    if (updateAmount.IsError)
	    {
		    errors.AddRange(updateAmount.Errors);
	    }

	    var addUsers = AddUsers(userIdsToAdd);
	    if (addUsers.IsError)
	    {
		    errors.AddRange(addUsers.Errors);
	    }

	    var userToRemove = RemoveUsers(userIdsToRemove);
	    if (userToRemove.IsError)
	    {
		    errors.AddRange(userToRemove.Errors);
	    }

	    return errors.Any() ? errors : Result.Updated;
    }

    private ErrorOr<Success> UpdateName(string name)
    {
	    if (name.IsNullEmptyOrWhitespace())
	    {
		    return PaymentErrors.NameCannotBeEmpty;
	    }

	    Name = name;
	    return  Result.Success;
    }

    private ErrorOr<Success> UpdateAmount(Amount amount)
    {
	    if (amount is null)
	    {
		    return  PaymentErrors.InvalidAmount;
	    }

	    Amount = amount;
	    return Result.Success;
    }

    private ErrorOr<Success> AddUsers(Guid[] userIdsToAdd)
    {
	    var existingDebtors = Debtors.Where(userIdsToAdd.Contains).ToArray();
	    if (existingDebtors.Any())
	    {
		    return existingDebtors.Select(PaymentErrors.UserIsAlreadyAdded).ToArray();
	    }

	    _debtors.AddRange(userIdsToAdd);
	    return Result.Success;
    }

    private ErrorOr<Success> RemoveUsers(Guid[] userIdsToRemove)
    {
	    var notExistingDebtors = userIdsToRemove.Where(id => !Debtors.Contains(id)).ToArray();
	    if (notExistingDebtors.Any())
	    {
		    return notExistingDebtors.Select(PaymentErrors.UserNotFound).ToArray();
	    }

	    _debtors.RemoveAll(userIdsToRemove.Contains);
	    return Result.Success;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Payment()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
