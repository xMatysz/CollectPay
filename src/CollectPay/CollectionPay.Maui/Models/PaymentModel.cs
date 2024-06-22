namespace CollectionPay.Maui.Models;

public class PaymentModel
{
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public string Currency { get; set; }

	public PaymentModel( string name, decimal amount, string currency)
	{
		Name = name;
		Amount = amount;
		Currency = currency;
	}

	public PaymentModel()
	{
	}
}