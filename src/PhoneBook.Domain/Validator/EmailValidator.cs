using System.Text.RegularExpressions;

namespace PhoneBook.Domain.Validator;

public static class EmailValidator
{
    private const int MaxEmailLength = 254;
    private const int MaxLocalPartLength = 64;

    // Covers the vast majority of real-world emails:
    // - local part: printable chars except @ and whitespace
    // - domain: labels separated by dots, each label alphanumeric + hyphens
    // - TLD: at least 2 chars
    private static readonly Regex EmailRegex = new(
        @"^(?=[^@\s]{1,64}@)" +       // local part: 1–64 chars before @
        @"[^\s@]+@" +                  // anything valid before @
        @"[a-zA-Z0-9]" +              // domain starts with alphanumeric
        @"[a-zA-Z0-9\-]*" +           // domain middle (may have hyphens)
        @"(\.[a-zA-Z0-9\-]+)*" +      // sub-domains
        @"\.[a-zA-Z]{2,}$",           // TLD: 2+ alpha chars
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static EmailValidationResult Validate(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return EmailValidationResult.Fail("Email cannot be empty.");

        if (email.Length > MaxEmailLength)
            return EmailValidationResult.Fail(
                $"Email must not exceed {MaxEmailLength} characters.");

        var atIndex = email.IndexOf('@');
        if (atIndex > MaxLocalPartLength)
            return EmailValidationResult.Fail(
                $"Local part of email must not exceed {MaxLocalPartLength} characters.");

        if (!EmailRegex.IsMatch(email))
            return EmailValidationResult.Fail($"'{email}' is not a valid email address.");

        return EmailValidationResult.Ok();
    }

    public static bool IsValid(string? email) => Validate(email).IsValid;
}

public sealed class EmailValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private EmailValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static EmailValidationResult Ok() => new(true, null);
    public static EmailValidationResult Fail(string message) => new(false, message);
}