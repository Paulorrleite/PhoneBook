using System.Text.RegularExpressions;

namespace PhoneBook.Domain.Exceptions;

public static class PhoneValidator
{
    private static readonly Regex PhoneRegex =
        new(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

    public static bool IsValid(Phone phone)
    {
        var completePhonenumber = $"+{phone.DDD}{phone.Number}";

        return PhoneRegex.IsMatch(completePhonenumber);
    }
}