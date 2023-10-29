using MediatR;

namespace CollectPay.Application.Common.Interactions;

public interface ICommand : IRequest
{
}

public interface ICommand<out T> : IRequest<T>
	where T : IResponse
{
}