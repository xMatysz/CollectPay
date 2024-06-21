using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.Dtos;

public record BillDto(Guid Id, string Name, Guid CreatorId, Guid[] Debtors)
{
	public BillDto(Bill bill)
		: this(bill.Id, bill.Name, bill.CreatorId, bill.Debtors.ToArray())
	{
	}
}