using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;

public class RemovePaymentCommandInputValidator : AbstractValidator<RemovePaymentCommand>
{
	public RemovePaymentCommandInputValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.UserId)));

		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.BillId)));

		RuleFor(x => x.PaymentId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.PaymentId)));
	}
}