using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed class CreateBillCommandHandler : CommandHandler<CreateBillCommand>
{
	private readonly IBillRepository _billRepository;

	public CreateBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	protected override async Task Process(CreateBillCommand command, CancellationToken cancellationToken)
	{
		var bill = new Bill(command.CreatorId, command.BillName, command.BuddyIds);

		await _billRepository.AddAsync(bill, cancellationToken);
	}
}