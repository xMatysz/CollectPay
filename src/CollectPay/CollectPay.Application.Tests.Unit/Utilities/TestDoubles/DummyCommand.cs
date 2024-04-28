using CollectPay.Application.Common.Abstraction;

namespace CollectPay.Application.Tests.Unit.Utilities.TestDoubles;

public class DummyCommand : ICommand<IErrorOr>, ICommand<ErrorOr<IErrorOr>>
{
}