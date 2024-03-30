namespace CollectionPay.Maui.Models;

public class PaymentCreateModel
{
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public string Currency { get; set; }
}