using PhoneBook.Domain.Entities.Abstraction;
using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;
using PhoneBook.Domain.Validator;

namespace PhoneBook.Domain;

public class Phone : Entity
{
    public string DDD { get; private set; }
    public string Number { get; private set; }
    public PhoneType PhoneType { get; private set; }

    private Phone(string DDD, string number, PhoneType phoneType)
    {
        this.DDD = DDD;
        Number = number;
        PhoneType = phoneType;
    }

    public static Phone CreatePhone(string DDD, string number, PhoneType phoneType)
    {
        var phone = new Phone(DDD, number, phoneType);

        if (!PhoneValidator.IsValid(phone))
            throw new DomainExceptions("Invalid phone number.");

        return phone;
    }
}
