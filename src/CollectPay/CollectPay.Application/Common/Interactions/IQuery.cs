using MediatR;

namespace CollectPay.Application.Common.Interactions;

public interface IQuery<out TResult> : IRequest<TResult>
{
}