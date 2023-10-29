using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.Common.Repositories;

public interface IBillRepository : IRepository<Bill, Guid>
{
}