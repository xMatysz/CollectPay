using CollectPay.Application.Common.Interactions;
using ErrorOr;

namespace CollectPay.Application.UnitTests.Utilities.TestDoubles;

public class DummyCommand : ICommand<IErrorOr>, ICommand<ErrorOr<IErrorOr>>
{
}