using System.Text.Json.Serialization;
using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed record CreateBillCommand(
	Guid CreatorId,
	string BillName) : ICommand<Created>;