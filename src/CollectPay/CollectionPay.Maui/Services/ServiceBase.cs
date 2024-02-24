namespace CollectionPay.Maui.Services;

public abstract class ServiceBase
{
	protected HttpClient HttpClient { get; }

	protected ServiceBase(HttpClient client)
	{
		HttpClient = client;
	}
}