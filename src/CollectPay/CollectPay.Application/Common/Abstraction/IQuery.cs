using ErrorOr;
using MediatR;

namespace CollectPay.Application.Common.Abstraction;

public interface IQuery<TResult> : IRequest<ErrorOr<TResult>>
{
}