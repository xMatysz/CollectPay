namespace CollectionPay.Contracts.Requests.Bill;

public record UpdateBillRequest(Guid BillId, string? Name);