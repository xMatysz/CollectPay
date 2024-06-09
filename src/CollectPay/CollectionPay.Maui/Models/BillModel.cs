namespace CollectionPay.Maui.Models;

public record BillModel(
	Guid Id,
	string Name,
	Guid[] Debtors,
	bool IsShared);