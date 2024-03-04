using CollectPay.Application.Common.Abstraction;

namespace CollectPay.Application.UnitTests.Utilities.TestDoubles;

public class DummyCommand : ICommand<IErrorOr>, ICommand<ErrorOr<IErrorOr>>
{
}