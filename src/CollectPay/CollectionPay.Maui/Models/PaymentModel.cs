namespace CollectionPay.Maui.Models;

public record PaymentModel(
	ImageSource Image,
	string Name,
	decimal Amount,
	string Currency);