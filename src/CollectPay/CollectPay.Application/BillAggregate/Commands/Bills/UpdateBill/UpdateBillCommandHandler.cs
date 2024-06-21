using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.UserAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

public class UpdateBillCommandHandler : ICommandHandler<UpdateBillCommand, Updated>
{
	private readonly IBillRepository _billRepository;
	private readonly IUserRepository _userRepository;

	public UpdateBillCommandHandler(IBillRepository billRepository, IUserRepository userRepository)
	{
		_billRepository = billRepository;
		_userRepository = userRepository;
	}

	public async Task<ErrorOr<Updated>> Handle(UpdateBillCommand request, CancellationToken cancellationToken = default)
	{
		var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);

		if (bill is null || !bill.Debtors.Contains(request.UserId))
		{
			return BillErrors.BillNotFound;
		}

		var emails = request.UpdateBillInfo.EmailsToAdd.Concat(request.UpdateBillInfo.EmailsToRemove).ToArray();

		var users = emails.Any()
			? await _userRepository.GetByEmails(emails, cancellationToken)
			: [];

		if (emails.Length != users.Length)
		{
			var usersEmails = users.Select(x => x.Email);
			var notFoundedUsers = emails.Where(email => !usersEmails.Contains(email));

			var errors = notFoundedUsers.Select(UserErrors.UserNotFound).ToList();
			return errors;
		}

		var userIdsToAdd = users
			.Where(us => request.UpdateBillInfo.EmailsToAdd.Contains(us.Email))
			.Select(x => x.Id)
			.ToArray();

		var userIdsToRemove = users
			.Where(us => request.UpdateBillInfo.EmailsToRemove.Contains(us.Email))
			.Select(x => x.Id)
			.ToArray();

		return bill.Update(request.UpdateBillInfo.Name, userIdsToAdd, userIdsToRemove);
	}
}