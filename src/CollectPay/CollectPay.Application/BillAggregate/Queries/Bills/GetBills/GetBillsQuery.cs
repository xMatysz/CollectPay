using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Dtos;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public sealed record GetBillsQuery(
	Guid UserId) : IQuery<BillDto[]>;