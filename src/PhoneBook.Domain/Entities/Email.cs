using PhoneBook.Domain.Exceptions;
using PhoneBook.Domain.Validator;
using PhoneBook.Shared.Entities;

namespace PhoneBook.Domain.Entities;

public class Email : Entity
{
    public string ContactEmail { get; private set; }

    public Email(string email)
    {
        if (!EmailValidator.IsValid(email))
            throw new DomainExceptions("Invalid email.");

        ContactEmail = email;
    }
}
