using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Commands.CreateBill;

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