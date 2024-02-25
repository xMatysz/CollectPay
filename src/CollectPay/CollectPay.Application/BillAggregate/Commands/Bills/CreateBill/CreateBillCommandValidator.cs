using FluentValidation;

namespace CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
{
	public CreateBillCommandValidator()
	{
		RuleFor(x => x.CreatorId)
			.NotEmpty();
	}
}