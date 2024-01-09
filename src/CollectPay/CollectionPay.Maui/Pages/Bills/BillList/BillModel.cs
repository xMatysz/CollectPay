namespace CollectionPay.Maui.Pages.Bills.BillList;

public class BillModel
{
	public Guid Id { get; set; }
	public Guid CreatorId { get; set; }
	public string Name { get; set; }

	public BillModel(Guid creatorId, string name)
	{
		CreatorId = creatorId;
		Name = name;
	}
}