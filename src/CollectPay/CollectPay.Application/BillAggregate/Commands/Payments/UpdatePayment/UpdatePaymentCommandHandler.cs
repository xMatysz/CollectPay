using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public class UpdatePaymentCommandHandler : ICommandHandler<UpdatePaymentCommand, Updated>
{
	private readonly IBillRepository _billRepository;

	public UpdatePaymentCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Updated>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken = default)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null)
		{
			return BillErrors.BillNotFound;
		}

		var payment = bill.Payments.FirstOrDefault(x => x.Id == request.PaymentId);

		if (payment is null)
		{
			return PaymentErrors.PaymentNotFound;
		}

		return payment.Update(request.UpdatePaymentInfo.CreatorId,
			request.UpdatePaymentInfo.IsCreatorIncluded,
			request.UpdatePaymentInfo.Amount,
			request.UpdatePaymentInfo.Debtors);
	}
}