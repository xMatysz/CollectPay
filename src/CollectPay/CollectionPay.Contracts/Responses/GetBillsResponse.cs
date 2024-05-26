namespace CollectionPay.Contracts.Responses;

public record GetBillsResponse(Guid Id, string Name, Guid CreatorId);