using MediatR;

namespace CollectPay.Application.Common.Abstraction;

public interface ICommand<out TResult> : IRequest<TResult>;