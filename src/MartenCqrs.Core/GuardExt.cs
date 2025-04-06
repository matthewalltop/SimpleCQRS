namespace MartenCqrs.Core;

using System.Text.RegularExpressions;

public static partial class GuardExt {
	
	public static Guid NullOrEmptyGuid(this IGuardClause guardClause, string paramName, Guid? guid) {
		if (guid == null || guid.Equals(Guid.Empty))
			throw new DomainException($"{paramName} cannot be empty.");

		return guid.Value;
	}

	public static string NullOrEmptyString(this IGuardClause guardClause, string paramName, string input)
	{
		if (string.IsNullOrEmpty(input))
			throw new DomainException($"{paramName} cannot be null or empty.");

		return input;
	}
	
	public static DateTime NullOrEmptyDateTime(this IGuardClause guardClause, string paramName, DateTime? input)
	{
		if (input == null || input.Equals(DateTime.MinValue) || input.Equals(DateTime.MaxValue))
			throw new DomainException($"{paramName} cannot be null or empty.");

		return input.Value;
	}
	
	public static decimal NegativeOrZero(this IGuardClause guardClause, string paramName, decimal input)
	{
		if (input <= 0.0m)
			throw new DomainException($"{paramName} cannot be negative or zero.");

		return input;
	}
	
	public static int NegativeOrZero(this IGuardClause guardClause, string paramName, int input)
	{
		if (input <= 0)
			throw new DomainException($"{paramName} cannot be negative or zero.");

		return input;
	}

	public static string InvalidStringFormat(this IGuardClause guardClause, string paramName, string input, string regex = @"^[a-zA-Z0-9_.\-/,()\\\s]+$", int maxLength = 500)
	{
		if (!Regex.IsMatch(input, regex))
			throw new DomainException($"{paramName} contains one or more invalid characters");
		if (input.Length > maxLength)
			throw new DomainException($"{paramName} is too long");

		return input;
	}
}