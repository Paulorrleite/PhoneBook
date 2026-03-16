using PhoneBook.Domain.Entities.Abstraction;
using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;

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
        var phone = new Phone(DDD, number, phoneType);

        if (!PhoneValidator.IsValid(phone))
            throw new DomainExceptions("Invalid phone number.");

        return new Phone(DDD, number, phoneType);
    }
}
