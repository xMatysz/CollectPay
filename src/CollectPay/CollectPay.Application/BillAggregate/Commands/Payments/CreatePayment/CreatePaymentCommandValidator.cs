using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
	public CreatePaymentCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Name)));

		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.BillId)));

		RuleFor(x => x.CreatorId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.CreatorId)));

		RuleFor(x => x.Amount)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Amount)));

		RuleFor(x => x.Debtors)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Debtors)));
	}
}