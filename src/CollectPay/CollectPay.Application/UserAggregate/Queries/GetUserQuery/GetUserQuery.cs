using CollectPay.Application.Common.Interactions;
using CollectPay.Domain.UserAggregate;

namespace CollectPay.Application.UserAggregate.Queries.GetUserQuery;

public record GetUserQuery(Guid UserId) : IQuery<ErrorOr<User>>;