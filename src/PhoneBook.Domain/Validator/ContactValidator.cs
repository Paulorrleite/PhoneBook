using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Exceptions.Phone;

namespace PhoneBook.Domain.Validator;

public class ContactValidator
{
    public static bool IsValid(string firstName,
                               string? lastName,
                               Phone phone,
                               Email? email,
                               DateOnly birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return false;
        
        if (phone is null)
            return false;

        if (email is null)
            return false;

        if (birthDate < DateOnly.FromDateTime(DateTime.Today).AddYears(-150))
            return false;

        return true;
    }
}
