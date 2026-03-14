using System.Text.RegularExpressions;

namespace PhoneBook.Domain.Validator;

public class EmailValidator
{
    private static readonly Regex EmailRegex =
       new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

    public static bool IsValid(string email)
    {
        return EmailRegex.IsMatch(email);
    }
}
