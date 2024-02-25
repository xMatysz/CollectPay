﻿using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate;

public class Bill : AggregateRoot
{
    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public Guid CreatorId { get; init; }
    public string Name { get; init; }

    public Bill(Guid creatorId, string name)
    {
	    Id = Guid.NewGuid();
	    CreatorId = creatorId;
        Name = name;
    }

    public ErrorOr<Updated> AddPayment(Payment newPayment)
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

    private Bill()
    {
    }
}