namespace CollectionPay.Maui.Models;

public record BillModel(
	Guid Id,
	string Name,
	ImageSource Image = null);