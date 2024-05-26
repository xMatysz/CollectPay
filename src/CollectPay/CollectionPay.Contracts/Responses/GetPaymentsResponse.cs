namespace CollectionPay.Contracts.Responses;

public record GetPaymentsResponse(Guid Id, string Name, decimal Amount, string Currency);