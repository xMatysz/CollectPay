namespace CollectionPay.Contracts.Requests.Bill;

public class UpdateBillRequest
{
	public Guid BillId { get; }
	public string Name { get; }
	public string[] DebtorsEmailsToAdd { get; }
	public string[] DebtorsEmailsToRemove { get; }

	public UpdateBillRequest(
		Guid billId,
		string? name = null,
		string[]? debtorsEmailsToAdd = default!,
		string[]? debtorsEmailsToRemove = default!)
	{
		BillId = billId;
		Name = name ?? string.Empty;
		DebtorsEmailsToAdd = debtorsEmailsToAdd ?? [];
		DebtorsEmailsToRemove = debtorsEmailsToRemove ?? [];
	}
}