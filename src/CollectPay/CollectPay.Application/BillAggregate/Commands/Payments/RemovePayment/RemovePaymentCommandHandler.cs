using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;

public class RemovePaymentCommandHandler : ICommandHandler<RemovePaymentCommand, Deleted>
{
	private readonly IBillRepository _billRepository;

	public RemovePaymentCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Deleted>> Handle(RemovePaymentCommand request, CancellationToken cancellationToken = default)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null || !bill.Debtors.Contains(request.UserId))
		{
			return BillErrors.BillNotFound;
		}

		return bill.RemovePayment(request.PaymentId);
	}
}