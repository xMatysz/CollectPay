﻿using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.Common.Repositories;

public interface IBillRepository
{
	Task AddAsync(Bill bill, CancellationToken cancellationToken = default);
	Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken = default);
}