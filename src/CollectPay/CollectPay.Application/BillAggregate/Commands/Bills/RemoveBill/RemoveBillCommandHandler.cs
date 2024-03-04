using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;

public class RemoveBillCommandHandler : ICommandHandler<RemoveBillCommand, Deleted>
{
	private readonly IBillRepository _billRepository;

	public RemoveBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Deleted>> Handle(RemoveBillCommand request, CancellationToken cancellationToken = default)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null)
		{
			return BillErrors.BillNotFound;
		}

		await _billRepository.RemoveAsync(bill, cancellationToken);
		return Result.Deleted;
	}
}