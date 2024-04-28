using CollectPay.Application.Behaviors;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Tests.Unit.Utilities.TestDoubles;

namespace CollectPay.Application.Tests.Unit.BehaviorsTests;

public class WhenUnitOfWorkBehaviorProcessing
{
	private readonly IUnitOfWork _sut;
	private readonly UnitOfWorkBehavior<DummyCommand, ErrorOr<IErrorOr>> _unitOfWorkBehavior;
	private readonly DummyCommand _dummyCommand;
	private readonly RequestHandlerDelegate<ErrorOr<IErrorOr>> _nextDelegate;

	public WhenUnitOfWorkBehaviorProcessing()
	{
		_sut = Substitute.For<IUnitOfWork>();
		_nextDelegate = Substitute.For<RequestHandlerDelegate<ErrorOr<IErrorOr>>>();
		_unitOfWorkBehavior = new UnitOfWorkBehavior<DummyCommand, ErrorOr<IErrorOr>>(_sut);
		_dummyCommand = new DummyCommand();
	}

	[Fact]
	public async Task ShouldCommitChangesOnSuccess()
	{
		_nextDelegate.Invoke().Returns(new ErrorOr<MediatR.Unit>());

		var result = await _unitOfWorkBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		result.IsError.Should().BeFalse();
		_sut.Received(1).BeginTransaction();
		await _sut.Received(1).FinishTransactionAsync();
		_sut.Received(0).RollbackTransaction();
	}

	[Fact]
	public async Task ShouldRollbackChangesOnException()
	{
		_nextDelegate.When(x => x.Invoke())
			.Do(_ => throw new Exception());

		var act = async () => await _unitOfWorkBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await act.Should().ThrowAsync<Exception>();
		await _sut.Received(0).FinishTransactionAsync();
		_sut.Received(1).BeginTransaction();
		_sut.Received(1).RollbackTransaction();
	}

	[Fact]
	public async Task ShouldRollbackChangesOnFailure()
	{
		var errorOr = Error.Conflict();
		_nextDelegate.Invoke().Returns(errorOr);

		var result = await _unitOfWorkBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		result.FirstError.Should().BeEquivalentTo(errorOr);
		await _sut.Received(0).FinishTransactionAsync();
		_sut.Received(1).BeginTransaction();
		_sut.Received(1).RollbackTransaction();
	}
}