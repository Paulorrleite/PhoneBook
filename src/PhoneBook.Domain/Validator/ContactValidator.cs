namespace PhoneBook.Domain.Validator;

public class ContactValidator
{
    public static bool IsValid(string firstName,
                               string? lastName,
                               Phone phone,
                               string? email,
                               DateOnly birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return false;

        if (phone is null)
            return false;

        if (email != null)
            if (!EmailValidator.IsValid(email))
                return false;

        if (birthDate < DateOnly.FromDateTime(DateTime.Today).AddYears(-150))
            return false;

        return true;
    }
}
