﻿using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;

public record GetPaymentsQuery(Guid BillId) : IQuery<Payment[]>;