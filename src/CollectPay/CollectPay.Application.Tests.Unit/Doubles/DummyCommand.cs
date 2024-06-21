using CollectPay.Application.Common.Abstraction;

namespace CollectPay.Application.Tests.Unit.Doubles;

public record DummyCommand : ICommand<ErrorOr<IErrorOr>>;