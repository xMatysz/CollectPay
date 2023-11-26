using System.Text.Json.Serialization;
using CollectPay.Application.Common.Interactions;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed record CreateBillCommand(
	Guid CreatorId,
	string BillName,
	List<Guid>? BuddyIds) : ICommand<ErrorOr<Created>>;