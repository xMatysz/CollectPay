using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public class UpdateBillCommandHandler : ICommandHandler<UpdateBillCommand, Updated>
{
	private readonly IBillRepository _billRepository;

	public UpdateBillCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Updated>> Handle(UpdateBillCommand request, CancellationToken cancellationToken = default)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null)
		{
			return BillErrors.BillNotFound;
		}

		// TODO: should check if user is added to bill
		if (bill.CreatorId != request.UserId)
		{
			return BillErrors.BillNotFound;
		}

		return bill.Update(request.UpdateBillInfo.Name);
	}
}