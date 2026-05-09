using FluentAssertions;
using PhoneBook.Domain;
using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;

namespace PhoneBook.DomainTests.Contacts;

public class ContactTests
{
    [Fact]
    public void CreateContact_EmptyFirstName_ShouldThrowError()
    {
        // Arrange
        var phone = Phone.CreatePhone("48", "999999999", PhoneType.Personal);
        var now = DateOnly.FromDateTime(DateTime.Now);

        // Act
        Action act = () => Contact.CreateContact("", null, phone, null, now);

        // Assert
        var exception = act.Should().Throw<DomainExceptions>().Which;
        exception.Message.Should().Be("First name can't be empty.");
    }

    [Fact]
    public void CreateContact_NullPhone_ShouldThrowError()
    {
        // Arrange
        Phone phone = null!;
        var now = DateOnly.FromDateTime(DateTime.Now);

        // Act
        Action act = () => Contact.CreateContact("Paulo", "Leite", phone, null, now);

        // Assert
        var exception = act.Should().Throw<DomainExceptions>().Which;
        exception.Message.Should().Be("Phone should be informed.");
    }

    [Fact]
    public void CreateContact_InvalidBirthDate_ShouldThrowError()
    {
        // Arrange
        var phone = Phone.CreatePhone("48", "999999999", PhoneType.Personal);
        var now = DateOnly.FromDateTime(DateTime.Now);

        // Act
        Action act = () => Contact.CreateContact("Paulo", "Leite", phone, null, now.AddYears(-151));

        // Assert
        var exception = act.Should().Throw<DomainExceptions>().Which;
        exception.Message.Should().Be("Invalid birthdate.");
    }
}
