namespace CollectPay.Tests.Shared.Builders;

public abstract class TestBuilder<TEntity>
{
	public abstract TEntity Build();

	protected void OverrideProperty<TId>(string propertyName, TEntity entity, TId value)
	{
		var prop = typeof(TEntity).GetProperty(propertyName);
		prop!.SetValue(entity, value);
	}
}