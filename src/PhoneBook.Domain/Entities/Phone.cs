using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;
using PhoneBook.Domain.Exceptions.Phone;
using PhoneBook.Shared.Entities;

namespace PhoneBook.Domain;

public class Phone : Entity
{
    public string DDD { get; private set; }
    public int Number { get; private set; }
    public PhoneType PhoneType { get; private set; }

    private Phone(string DDD, int number, PhoneType phoneType)
    {
        this.DDD = DDD;
        Number = number;
        PhoneType = phoneType;
    }

    public Phone CreatePhone(string DDD, int number, PhoneType phoneType)
    {
        var phoneNumber = DDD + number.ToString();

        if (!PhoneValidator.IsValid(phoneNumber))
            throw new DomainExceptions("Invalid phone number.");

        return new Phone(DDD, number, phoneType);
    }
}
