using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

public sealed class CreateBillCommandHandler : ICommandHandler<CreateBillCommand, Created>
{
	private readonly IBillRepository _billRepository;

	public CreateBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Created>> Handle(CreateBillCommand command, CancellationToken cancellationToken = default)
	{
		var bill = new Bill(command.CreatorId, command.BillName);

		await _billRepository.AddAsync(bill, cancellationToken);

		return Result.Created;
	}
}