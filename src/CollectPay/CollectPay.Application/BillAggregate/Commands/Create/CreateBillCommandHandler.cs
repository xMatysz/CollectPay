using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.BillAggregate.Commands.Create;

public sealed class CreateBillCommandHandler : ICommandHandler<CreateBillCommand>
{
	private readonly IBillRepository _billRepository;

	public CreateBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public Task Handle(CreateBillCommand command, CancellationToken cancellationToken)
	{
		var bill = new Bill(command.CreatorId, command.BillName, command.BuddyIds);

		_billRepository.Add(bill);
		return Task.CompletedTask;
	}
}