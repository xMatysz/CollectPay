﻿using CollectPay.Application.Common.Interactions;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed record CreateBillCommand(
	Guid CreatorId,
	string BillName,
	List<Guid> BuddyIds) : ICommand;