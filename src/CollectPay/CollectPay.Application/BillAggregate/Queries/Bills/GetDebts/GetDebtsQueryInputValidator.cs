using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;

public class GetDebtsQueryInputValidator : AbstractValidator<GetDebtsQuery>
{
	public GetDebtsQueryInputValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(GetDebtsQuery.UserId)));

		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(GetDebtsQuery.BillId)));
	}
}