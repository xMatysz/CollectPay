using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;

public record GetDebtsQuery(Guid BillId, Guid UserId) : IQuery<Debt[]>;