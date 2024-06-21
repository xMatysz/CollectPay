using CollectPay.Application.Behaviors;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Tests.Unit.Doubles;

namespace CollectPay.Application.Tests.Unit.BehaviorsTests;

public class WhenProcessingUnitOfWorkBehavior
{
	private readonly UnitOfWorkBehavior<DummyCommand, ErrorOr<IErrorOr>> _behavior;
	private readonly IUnitOfWork _unitOfWork;
	private readonly RequestHandlerDelegate<ErrorOr<IErrorOr>> _nextDelegate;
	private readonly DummyCommand _dummyCommand;

	public WhenProcessingUnitOfWorkBehavior()
	{
		_dummyCommand = new();
		_unitOfWork = Substitute.For<IUnitOfWork>();
		_behavior = new UnitOfWorkBehavior<DummyCommand, ErrorOr<IErrorOr>>(_unitOfWork);
		_nextDelegate = Substitute.For<RequestHandlerDelegate<ErrorOr<IErrorOr>>>();
	}

	[Fact]
	public async Task Should_BeginTransactionOnce()
	{
		await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		_unitOfWork.Received(1).BeginTransaction();
	}

	[Fact]
	public async Task Should_RollbackOnException()
	{
		_nextDelegate
			.When(x => x.Invoke())
			.Do(_ => throw new Exception());

		var act = async () => await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await act.Should().ThrowAsync<Exception>();
		await _unitOfWork.DidNotReceive().FinishTransactionAsync();
		await _unitOfWork.Received(1).RollbackTransactionAsync();
	}

	[Fact]
	public async Task Should_RollbackOnFail()
	{
		_nextDelegate
			.Invoke()
			.Returns(Error.Failure());

		await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await _unitOfWork.DidNotReceive().FinishTransactionAsync();
		await _unitOfWork.Received(1).RollbackTransactionAsync();
	}

	[Fact]
	public async Task Should_FinishTransactionOnSuccess()
	{
		_nextDelegate
			.Invoke()
			.Returns(new ErrorOr<Success>());

		await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await _unitOfWork.Received(1).FinishTransactionAsync();
		await _unitOfWork.DidNotReceive().RollbackTransactionAsync();
	}
}