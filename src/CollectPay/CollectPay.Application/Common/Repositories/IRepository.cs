namespace CollectPay.Application.Common.Repositories;

public interface IRepository<T, in TId>
{
	Task<T[]> GetAll();
	T? GetById(TId id);
	void Add(T item);
}