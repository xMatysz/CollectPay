using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Application.BillAggregate.Queries.GetPayments;

public record GetPaymentsQuery(Guid BillId) : IQuery<Payment[]>;