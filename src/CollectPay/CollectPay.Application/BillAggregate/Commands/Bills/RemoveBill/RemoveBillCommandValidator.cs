using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;

public class RemoveBillCommandValidator : AbstractValidator<RemoveBillCommand>
{
	public RemoveBillCommandValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(RemoveBillCommand.UserId)));

		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(RemoveBillCommand.BillId)));
	}
}