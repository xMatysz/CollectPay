using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public Guid CreatorId { get; init; }
    public string Name { get; set; }

    public Bill(Guid creatorId, string name)
    {
	    Id = Guid.NewGuid();
	    CreatorId = creatorId;
        Name = name;
    }

    public ErrorOr<Updated> Update(string? name)
    {
	    if (name is null or "")
	    {
		    return BillErrors.BillNameCannotBeEmpty;
	    }

	    Name = name;

	    return Result.Updated;
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Bill()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}