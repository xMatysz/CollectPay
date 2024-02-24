namespace CollectionPay.Contracts.Requests.Bill;

public record CreateBillRequest(Guid UserId, string Name);