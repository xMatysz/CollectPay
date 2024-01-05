namespace CollectionPay.Contracts.Requests;

public record CreateBillRequest(Guid UserId, string Name);