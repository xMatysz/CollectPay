﻿using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;

public class GetPaymentsQueryHandler : IQueryHandler<GetPaymentsQuery, Payment[]>
{
	private readonly IBillRepository _billRepository;

	public GetPaymentsQueryHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Payment[]>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null || !bill.Debtors.Contains(request.UserId))
		{
			return BillErrors.BillNotFound;
		}

		return bill.Payments.ToArray();
	}
}