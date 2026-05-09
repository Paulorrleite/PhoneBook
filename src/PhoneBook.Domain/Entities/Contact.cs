using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Entities.Abstraction;
using PhoneBook.Domain.Exceptions;
using PhoneBook.Domain.Validator;

namespace PhoneBook.Domain;

public class Contact : Entity
{
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Phone Phone { get; private set; }
    public string? Email { get; private set; }
    public DateOnly BirthDate { get; private set; }

    private Contact(string firstName,
                    string? lastName,
                    Phone phone,
                    string? email,
                    DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        BirthDate = birthDate;
    }

    public static Contact CreateContact(
    string firstName,
    string? lastName,
    Phone phone,
    string? email,
    DateOnly birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainExceptions("First name can't be empty.");

        if (phone is null)
            throw new DomainExceptions("Phone should be informed.");
        if (!PhoneValidator.IsValid(phone))
            throw new DomainExceptions("Invalid phone number format.");

        if (email != null && !EmailValidator.IsValid(email))
            throw new DomainExceptions("Invalid email.");

        if (birthDate < DateOnly.FromDateTime(DateTime.Today).AddYears(-150) || birthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new DomainExceptions("Invalid birthdate.");

        return new Contact(firstName, lastName, phone, email, birthDate);
    }

    public void UpdateContact(string? firstName,
                              string? lastName,
                              Phone? phone,
                              string? email,
                              DateOnly? birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (firstName != null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainExceptions("First name cannot be empty.");

            FirstName = firstName;
        }

        if (lastName != null)
        {
            LastName = lastName;
        }

        if (phone != null)
        {
            if (!PhoneValidator.IsValid(phone))
                throw new DomainExceptions("Invalid phone number format.");

            Phone = phone;
        }

        if (email != null)
        {
            if (!EmailValidator.IsValid(email))
                throw new DomainExceptions("Invalid email format.");

            Email = email;
        }

        if (birthDate != null)
        {
            if (birthDate > today)
                throw new DomainExceptions("Birth date cannot be in the future.");

            BirthDate = birthDate.Value;
        }
    }
}
