using CollectPay.Application.Common;
using FluentValidation;

namespace CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;

public class GetPaymentsQueryInputValidator : AbstractValidator<GetPaymentsQuery>
{
	public GetPaymentsQueryInputValidator()
	{
		RuleFor(x => x.BillId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(GetPaymentsQuery.BillId)));

		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage(ValidationMessages.PropertyIsRequired(nameof(GetPaymentsQuery.UserId)));
	}
}