// PhoneValidator.cs
using System.Text.RegularExpressions;

namespace PhoneBook.Domain.Validator;

public static class PhoneValidator
{
    private static readonly Regex OnlyDigitsRegex =
        new(@"^\d+$", RegexOptions.Compiled);

    // E.164: +[1-9][digits], total 7–15 chars (DDD + Number)
    private static readonly Regex E164Regex =
        new(@"^\+[1-9]\d{6,14}$", RegexOptions.Compiled);

    public static PhoneValidationResult Validate(Phone phone)
    {
        if (phone is null)
            return PhoneValidationResult.Fail("Phone cannot be null.");

        if (string.IsNullOrWhiteSpace(phone.DDD))
            return PhoneValidationResult.Fail("DDD cannot be empty.");

        if (string.IsNullOrWhiteSpace(phone.Number))
            return PhoneValidationResult.Fail("Number cannot be empty.");

        if (!OnlyDigitsRegex.IsMatch(phone.DDD))
            return PhoneValidationResult.Fail("DDD must contain only numeric characters.");

        if (!OnlyDigitsRegex.IsMatch(phone.Number))
            return PhoneValidationResult.Fail("Number must contain only numeric characters.");

        if (phone.Number.Length < 4 || phone.Number.Length > 13)
            return PhoneValidationResult.Fail("Number must be between 4 and 13 digits.");

        if (phone.DDD.Length < 1 || phone.DDD.Length > 3)
            return PhoneValidationResult.Fail("DDD must be between 1 and 3 digits.");

        var full = $"+{phone.DDD}{phone.Number}";

        if (!E164Regex.IsMatch(full))
            return PhoneValidationResult.Fail($"Phone number '{full}' is not a valid E.164 number.");

        return PhoneValidationResult.Ok();
    }

    // Convenience overload that keeps backward compatibility
    public static bool IsValid(Phone phone) => Validate(phone).IsValid;
}

public sealed class PhoneValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private PhoneValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static PhoneValidationResult Ok() => new(true, null);
    public static PhoneValidationResult Fail(string message) => new(false, message);
}