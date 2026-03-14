namespace PhoneBook.Domain.Exceptions;

internal class DomainExceptions : Exception
{
    public DomainExceptions(string message) : base(message)
    {
    }
}
