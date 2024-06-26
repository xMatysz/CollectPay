﻿using CollectionPay.Contracts;
using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Responses;
using CollectPay.Api.Authentication;
using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class BillsController : ApiController
{
	public BillsController(ISender sender)
		: base(sender)
	{
	}

	[HttpGet(BillRoutes.List)]
	public async Task<IActionResult> GetBills()
	{
		var userId = HttpContext.GetUserId();

		var results = await Sender.Send(new GetBillsQuery(userId));

		return results.Match(
			list =>
				Ok(list.Select(dto =>
					new GetBillsResponse(
						dto.Id,
						dto.Name,
						dto.CreatorId,
						dto.Debtors.ToArray()))),
			Problem);
	}

	[HttpPost(BillRoutes.Create)]
	public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
	{
		var userId = HttpContext.GetUserId();

		var command = new CreateBillCommand(
			userId,
			request.Name);

		var result = await Sender.Send(command);

		// TODO: fix location
		return result.Match(
			val => Created(BillRoutes.List, val),
			Problem);
	}

	[HttpPost(BillRoutes.Update)]
	public async Task<IActionResult> UpdateBill([FromBody] UpdateBillRequest request)
	{
		var userId = HttpContext.GetUserId();

		var command = new UpdateBillCommand(
			request.BillId,
			userId,
			new UpdateBillInfo(
				request.Name,
				request.DebtorsEmailsToAdd,
				request.DebtorsEmailsToRemove));

		var result = await Sender.Send(command);

		return result.Match(
			val => Ok(val),
			Problem);
	}

	[HttpDelete(BillRoutes.Remove)]
	public async Task<IActionResult> RemoveBill([FromQuery] Guid billId)
	{
		var userId = HttpContext.GetUserId();

		var command = new RemoveBillCommand(userId, billId);

		var result = await Sender.Send(command);

		return result.Match(
			_ => Ok(),
			Problem);
	}

	[HttpGet(BillRoutes.Debts)]
	public async Task<IActionResult> GetDebts([FromQuery] Guid billId)
	{
		var results = await Sender.Send(new GetDebtsQuery(HttpContext.GetUserId(), billId));

		return results.Match(
			list =>
				Ok(list.Select(debt =>
					new GetDebtsResponse(
						debt.Debtor,
						debt.DebtAmount,
						debt.Creditor))),
			Problem);
	}
}
