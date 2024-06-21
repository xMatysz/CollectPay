namespace CollectPay.Core.Extensions;

public static class StringExtensions
{
	public static bool IsNullEmptyOrWhitespace(this string? str) => string.IsNullOrWhiteSpace(str);
}