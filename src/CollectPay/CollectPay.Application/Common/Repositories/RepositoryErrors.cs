namespace CollectPay.Application.Common.Repositories;

public static class RepositoryErrors
{
	public static Error EntityWithIdNotFound(string entityType, string id) => Error.NotFound(
		code: "EntityWithSpecifiedIdIsNotFound",
		description: $"{entityType} with {id} is not found");

}