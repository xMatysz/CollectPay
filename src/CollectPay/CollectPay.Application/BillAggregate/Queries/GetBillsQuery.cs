using CollectPay.Application.Common.Interactions;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Queries;

public sealed record GetBillsQuery : IQuery<List<Bill>>;