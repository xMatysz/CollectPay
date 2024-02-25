using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;

public class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand, Created>
{
	private readonly IBillRepository _billRepository;

	public CreatePaymentCommandHandler(IBillRepository billRepository)
	{
		_billRepository = billRepository;
	}

	public async Task<ErrorOr<Created>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken = default)
	{
		var payment = Payment.Create(request.BillId,request.CreatorId, request.IsCreatorIncluded, request.Amount, request.Debtors);

		if (payment.IsError)
		{
			return payment.Errors;
		}

		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null)
		{
			return BillErrors.BillNotFound;
		}

		var result = bill.AddPayment(payment.Value);

		if (result.IsError)
		{
			return result.Errors;
		}

		return Result.Created;
	}
}