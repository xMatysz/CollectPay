using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
{
	public CreateBillCommandValidator()
	{
		RuleFor(x => x.CreatorId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreateBillCommand.CreatorId)));

		RuleFor(x => x.BillName)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreateBillCommand.BillName)));
	}
}