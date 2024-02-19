using ErrorOr;
using MediatR;

namespace CollectPay.Application.Common.Abstraction;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, ErrorOr<TResult>>
	where TQuery : IQuery<TResult>;