using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

public class GetBillsQueryInputValidator : AbstractValidator<GetBillsQuery>
{
	public GetBillsQueryInputValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(GetBillsQuery.UserId)));
	}
}