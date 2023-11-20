using CollectPay.Application.Behaviors;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.UnitTests.Utilities.TestDoubles;

namespace CollectPay.Application.UnitTests.BehaviorsTests;

public class WhenUnitOfWorkBehaviorProcessing
{
	private readonly IUnitOfWork _sut;
	private readonly UnitOfWorkBehavior<DummyCommand, Unit> _unitOfWorkBehavior;
	private readonly DummyCommand _dummyCommand;
	private readonly RequestHandlerDelegate<Unit> _nextDelegate;

	public WhenUnitOfWorkBehaviorProcessing()
	{
		_sut = Substitute.For<IUnitOfWork>();
		_nextDelegate = Substitute.For<RequestHandlerDelegate<Unit>>();
		_unitOfWorkBehavior = new UnitOfWorkBehavior<DummyCommand, Unit>(_sut);
		_dummyCommand = new DummyCommand();
	}

	[Fact]
	public async Task ShouldCommitChangesOnSuccess()
	{
		_nextDelegate.Invoke().Returns(Unit.Value);

		await _unitOfWorkBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		_sut.Received(1).BeginTransaction();
		await _sut.Received(1).CommitAsync();
		_sut.Received(0).RollbackTransaction();
	}

	[Fact]
	public async Task ShouldRollbackChangesOnFail()
	{
		_nextDelegate.When(x => x.Invoke())
			.Do(_ => throw new Exception());

		await _unitOfWorkBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		_sut.Received(1).BeginTransaction();
		await _sut.Received(0).CommitAsync();
		_sut.Received(1).RollbackTransaction();
	}
}