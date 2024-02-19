using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Queries.GetBills;

public sealed record GetBillsQuery : IQuery<List<Bill>>;