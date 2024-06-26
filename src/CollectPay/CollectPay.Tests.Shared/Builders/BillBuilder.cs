﻿using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Tests.Shared.Builders;

public class BillBuilder : TestBuilder<Bill>
{
	private Guid _creatorId = DefaultCreatorId;
	private string _name = DefaultName;
	private  Payment[] _payments = [];
	private Guid[] _debtors = [];

	public static Guid DefaultCreatorId { get; } = Guid.NewGuid();
	public static string DefaultName => "Test Bill";

	public static BillBuilder Default()
	{
		return new BillBuilder();
	}

	public  BillBuilder WithCreatorId(Guid creatorId)
	{
		_creatorId = creatorId;
		return this;
	}

	public BillBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public BillBuilder WithPayments(Payment[] payments)
	{
		_payments = payments;
		return this;
	}

	public BillBuilder WithDebtors(Guid[] debtors)
	{
		_debtors = debtors;
		return this;
	}

	public override Bill Build()
	{
		var bill = new Bill(_creatorId, _name);

		foreach (var payment in _payments)
		{
			bill.AddPayment(payment);
		}

		bill.Update(bill.Name, _debtors, []);

		return bill;
	}

	public Bill BuildWithDebtors(Guid[] debtorIds)
	{
		var bill = Build();
		bill.Update(bill.Name, debtorIds, []);
		return bill;
	}
}