using ErrorOr;
using MediatR;

namespace CollectPay.Application.Common.Abstraction;

public interface ICommand<TResult> : IRequest<ErrorOr<TResult>>;