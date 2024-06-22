namespace CollectionPay.Contracts;

public static class BillRoutes
{
	public const string List = "bills";
	public const string Create = "bills/create";
	public const string Update = "bills/update";
	public const string Remove = "bills/remove";
	public const string Debts = "bills/debts";
}


public static class PaymentRoutes
{
	public const string List = "payments";
	public const string Create = "payments/create";
	public const string Update = "payments/update";
	public const string Remove = "payments/remove";
}


public static class UserRoutes
{
	public const string Register = "/users/register";
	public const string Login = "/users/login";
}