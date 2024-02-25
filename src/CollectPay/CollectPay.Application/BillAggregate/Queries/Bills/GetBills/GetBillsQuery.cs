using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public sealed record GetBillsQuery : IQuery<List<Bill>>;