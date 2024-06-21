using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;

public class UpdatePaymentCommandInputValidator : AbstractValidator<UpdatePaymentCommand>
{
	public UpdatePaymentCommandInputValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.UserId)));

		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.BillId)));

		RuleFor(x => x.PaymentId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.PaymentId)));

		RuleFor(x => x.UpdatePaymentInfo)
			.NotNull()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.UpdatePaymentInfo)));
	}
}