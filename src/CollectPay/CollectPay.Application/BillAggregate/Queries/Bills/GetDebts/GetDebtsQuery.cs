using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;

public record GetDebtsQuery(Guid UserId, Guid BillId) : IQuery<Debt[]>;