using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate;

public class SpecialId
{
	public int Id { get; set; }
	public Guid Value { get; set; }

	public SpecialId(Guid value)
	{
		Value = value;
	}
}

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments = [];
    private readonly List<Guid> _debtors = [];

    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();
    public IReadOnlyCollection<Guid> Debtors => _debtors.AsReadOnly();
    public List<SpecialId> Debtors2 { get; set; } = [];

    public Guid CreatorId { get; init; }
    public string Name { get; set; }

    public Bill(Guid creatorId, string name)
    {
	    Id = Guid.NewGuid();
	    CreatorId = creatorId;
        Name = name;
    }

    public ErrorOr<Updated> AddPayment(Payment? newPayment)
    {
	    if (newPayment is null)
	    {
		    return PaymentErrors.InvalidPayment;
	    }

	    if (Payments.Any(x => Equals(x, newPayment)))
	    {
		    return PaymentErrors.PaymentAlreadyExist;
	    }

	    _payments.Add(newPayment);
	    return Result.Updated;
    }

    public ErrorOr<Deleted> RemovePayment(Guid id)
    {
	    var itemToRemove = _payments.FirstOrDefault(x => x.Id == id);

	    if (itemToRemove is null)
	    {
		    return PaymentErrors.PaymentNotFound;
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
	    if (name is null or "" || name == Name)
	    {
		    return Result.Success;
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
	    Debtors2.Add(new SpecialId(userIdsToAdd[0]));
	    return Result.Success;
    }

    private ErrorOr<Success> RemoveDebtors(Guid[] userIdsToRemove)
    {
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