using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Exceptions;
using PhoneBook.Domain.Validator;
using PhoneBook.Shared.Entities;

namespace PhoneBook.Domain;

public class Contact : Entity
{
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Phone Phone { get; private set; }
    public Email? Email { get; private set; }
    public DateOnly BirthDate { get; private set; }

    private Contact(string firstName,
                    string? lastName,
                    Phone phone,
                    Email email,
                    DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        BirthDate = birthDate;
    }

    public Contact CreateContact(string firstName,
                                 string? lastName,
                                 Phone phone,
                                 Email email,
                                 DateOnly birthDate)
    {
        if (!ContactValidator.IsValid(firstName, lastName, phone, email, birthDate))
            throw new DomainExceptions("Invalid contact data.");  

            return new Contact(firstName, lastName, phone, email, birthDate);
    }

    public void UpdateContact(string? firstName,
                              string? lastName,
                              Phone? phone,
                              Email? email,
                              DateOnly? birthDate)
    {
        if (firstName != null && !string.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName;
        }

        

        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        BirthDate = birthDate;
    }
}
