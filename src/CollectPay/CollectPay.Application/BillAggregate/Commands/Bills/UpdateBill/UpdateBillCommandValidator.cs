using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public class UpdateBillCommandValidator : AbstractValidator<UpdateBillCommand>
{
	public UpdateBillCommandValidator()
	{
		RuleFor(x=>x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.UserId)));

		RuleFor(x=>x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.BillId)));


		RuleFor(x=>x.UpdateBillInfo)
			.NotNull()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.UpdateBillInfo)));
	}
}