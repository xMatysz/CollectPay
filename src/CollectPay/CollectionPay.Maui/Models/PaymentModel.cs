﻿namespace CollectionPay.Maui.Models;

public class PaymentModel
{
	public ImageSource Image { get; set; }
	public string Name { get; set; }
	public decimal Amount { get; set; }
	public string Currency { get; set; }

	public PaymentModel(ImageSource image, string name, decimal amount, string currency)
	{
		Image = image;
		Name = name;
		Amount = amount;
		Currency = currency;
	}

	public PaymentModel()
	{
	}
}