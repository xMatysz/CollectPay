using CollectionPay.Contracts.Requests;
using CollectPay.Application.BillAggregate.Commands.Create;
using Mapster;

namespace CollectPay.Api.Mapping;

public class CreateBillMapping : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateBillRequest, CreateBillCommand>()
			.Map(dest => dest.CreatorId, src => src.UserId)
			.Map(dest => dest.BillName, src => src.Name)
			.Map(dest => dest.BuddyIds, src => new List<Guid>());
	}
}