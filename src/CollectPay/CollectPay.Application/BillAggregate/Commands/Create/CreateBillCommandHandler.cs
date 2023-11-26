using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using ErrorOr;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed class CreateBillCommandHandler : CommandHandler<CreateBillCommand, ErrorOr<Created>>
{
	private readonly IBillRepository _billRepository;

	public CreateBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	protected override async Task<ErrorOr<Created>> Process(CreateBillCommand command, CancellationToken cancellationToken)
	{
		var bill = new Bill(command.CreatorId, command.BillName);

		await _billRepository.AddAsync(bill, cancellationToken);

		return Result.Created;
	}
}