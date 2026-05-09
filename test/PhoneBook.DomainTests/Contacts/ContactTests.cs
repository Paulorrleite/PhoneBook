using FluentAssertions;
using PhoneBook.Domain;
using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;
using PhoneBook.DomainTests.Builder;

namespace PhoneBook.DomainTests;

public class ContactTests
{
    // ── Success ───────────────────────────────────────────────────────────────

    [Fact]
    public void CreateContact_ValidAllFields_ShouldReturnContact()
    {
        var phone = PhoneBuilder.Valid();
        var birthDate = new DateOnly(1990, 6, 15);

        var contact = Contact.CreateContact("Jane", "Doe", phone, "jane@example.com", birthDate);

        contact.Should().NotBeNull();
        contact.FirstName.Should().Be("Jane");
        contact.LastName.Should().Be("Doe");
        contact.Phone.Should().Be(phone);
        contact.Email.Should().Be("jane@example.com");
        contact.BirthDate.Should().Be(birthDate);
    }

    [Fact]
    public void CreateContact_NullLastName_ShouldReturnContactWithNullLastName()
    {
        var contact = Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, new DateOnly(1990, 1, 1));

        contact.LastName.Should().BeNull();
    }

    [Fact]
    public void CreateContact_NullEmail_ShouldReturnContactWithNullEmail()
    {
        var contact = Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, new DateOnly(1990, 1, 1));

        contact.Email.Should().BeNull();
    }

    [Fact]
    public void CreateContact_BirthDateToday_ShouldReturnContact()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var contact = Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, today);

        contact.BirthDate.Should().Be(today);
    }

    [Fact]
    public void CreateContact_BirthDateExactly150YearsAgo_ShouldReturnContact()
    {
        // Exact boundary: exactly 150 years ago should be accepted (not less than limit)
        var boundary = DateOnly.FromDateTime(DateTime.Today).AddYears(-150);

        var contact = Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, boundary);

        contact.BirthDate.Should().Be(boundary);
    }

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("user.name+tag@sub.domain.org")]
    [InlineData("USER@EXAMPLE.COM")]
    [InlineData("123@numbers.io")]
    public void CreateContact_ValidEmailFormats_ShouldReturnContact(string validEmail)
    {
        var contact = Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), validEmail, new DateOnly(1990, 1, 1));

        contact.Email.Should().Be(validEmail);
    }

    // ── FirstName failures ────────────────────────────────────────────────────

    [Fact]
    public void CreateContact_EmptyFirstName_ShouldThrowDomainException()
    {
        var act = () => Contact.CreateContact("", null, PhoneBuilder.Valid(), null, new DateOnly(1990, 1, 1));

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*First name*");
    }

    [Fact]
    public void CreateContact_WhitespaceOnlyFirstName_ShouldThrowDomainException()
    {
        var act = () => Contact.CreateContact("   ", null, PhoneBuilder.Valid(), null, new DateOnly(1990, 1, 1));

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*First name*");
    }

    [Fact]
    public void CreateContact_NullFirstName_ShouldThrowDomainException()
    {
        var act = () => Contact.CreateContact(null!, null, PhoneBuilder.Valid(), null, new DateOnly(1990, 1, 1));

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*First name*");
    }

    // ── Phone failures ────────────────────────────────────────────────────────

    [Fact]
    public void CreateContact_NullPhone_ShouldThrowDomainException()
    {
        var act = () => Contact.CreateContact("Jane", null, null!, null, new DateOnly(1990, 1, 1));

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*Phone*");
    }

    // ── Email failures ────────────────────────────────────────────────────────

    [Theory]
    [InlineData("notanemail")]
    [InlineData("missing@")]
    [InlineData("@nodomain.com")]
    [InlineData("spaces in@email.com")]
    [InlineData("double@@domain.com")]
    [InlineData("no-at-sign")]
    public void CreateContact_InvalidEmailFormat_ShouldThrowDomainException(string invalidEmail)
    {
        var act = () => Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), invalidEmail, new DateOnly(1990, 1, 1));

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*email*");
    }

    // ── BirthDate failures ────────────────────────────────────────────────────

    [Fact]
    public void CreateContact_BirthDateInTheFuture_ShouldThrowDomainException()
    {
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

        var act = () => Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, tomorrow);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*birthdate*");
    }

    [Fact]
    public void CreateContact_BirthDateFarFuture_ShouldThrowDomainException()
    {
        var farFuture = new DateOnly(2999, 12, 31);

        var act = () => Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, farFuture);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*birthdate*");
    }

    [Fact]
    public void CreateContact_BirthDateOlderThan150Years_ShouldThrowDomainException()
    {
        var tooOld = DateOnly.FromDateTime(DateTime.Today).AddYears(-150).AddDays(-1);

        var act = () => Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, tooOld);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*birthdate*");
    }

    [Fact]
    public void CreateContact_BirthDateExactly150YearsAndOneDayAgo_ShouldThrowDomainException()
    {
        // One day past the boundary must be rejected
        var justOutsideBoundary = DateOnly.FromDateTime(DateTime.Today).AddYears(-150).AddDays(-1);

        var act = () => Contact.CreateContact("Jane", null, PhoneBuilder.Valid(), null, justOutsideBoundary);

        act.Should().Throw<DomainExceptions>();
    }

    // ── Success — single field updates ────────────────────────────────────────

    [Fact]
    public void UpdateContact_ValidFirstName_ShouldUpdateFirstName()
    {
        var contact = ContactBuilder.Valid();

        contact.UpdateContact("Alice", null, null, null, null);

        contact.FirstName.Should().Be("Alice");
    }

    [Fact]
    public void UpdateContact_ValidLastName_ShouldUpdateLastName()
    {
        var contact = ContactBuilder.Valid();

        contact.UpdateContact(null, "Smith", null, null, null);

        contact.LastName.Should().Be("Smith");
    }

    [Fact]
    public void UpdateContact_ValidPhone_ShouldUpdatePhone()
    {
        var contact = ContactBuilder.Valid();
        var newPhone = Phone.CreatePhone("21", "912345678", PhoneType.Residential);

        contact.UpdateContact(null, null, newPhone, null, null);

        contact.Phone.Should().Be(newPhone);
    }

    [Fact]
    public void UpdateContact_ValidEmail_ShouldUpdateEmail()
    {
        var contact = ContactBuilder.Valid();

        contact.UpdateContact(null, null, null, "new@email.com", null);

        contact.Email.Should().Be("new@email.com");
    }

    [Fact]
    public void UpdateContact_ValidBirthDate_ShouldUpdateBirthDate()
    {
        var contact = ContactBuilder.Valid();
        var newDate = new DateOnly(1985, 3, 20);

        contact.UpdateContact(null, null, null, null, newDate);

        contact.BirthDate.Should().Be(newDate);
    }

    [Fact]
    public void UpdateContact_BirthDateIsToday_ShouldUpdateBirthDate()
    {
        var contact = ContactBuilder.Valid();
        var today = DateOnly.FromDateTime(DateTime.Today);

        contact.UpdateContact(null, null, null, null, today);

        contact.BirthDate.Should().Be(today);
    }

    // ── Success — compound / edge updates ─────────────────────────────────────

    [Fact]
    public void UpdateContact_AllValidFields_ShouldUpdateAllFields()
    {
        var contact = ContactBuilder.Valid();
        var newPhone = Phone.CreatePhone("21", "912345678", PhoneType.Residential);
        var newDate = new DateOnly(2000, 12, 31);

        contact.UpdateContact("Bob", "Brown", newPhone, "bob@brown.com", newDate);

        contact.FirstName.Should().Be("Bob");
        contact.LastName.Should().Be("Brown");
        contact.Phone.Should().Be(newPhone);
        contact.Email.Should().Be("bob@brown.com");
        contact.BirthDate.Should().Be(newDate);
    }

    [Fact]
    public void UpdateContact_AllNullParameters_ShouldNotChangeAnyField()
    {
        var contact = ContactBuilder.Valid();
        var snapshot = new
        {
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.BirthDate,
            contact.Phone
        };

        contact.UpdateContact(null, null, null, null, null);

        contact.FirstName.Should().Be(snapshot.FirstName);
        contact.LastName.Should().Be(snapshot.LastName);
        contact.Email.Should().Be(snapshot.Email);
        contact.BirthDate.Should().Be(snapshot.BirthDate);
        contact.Phone.Should().Be(snapshot.Phone);
    }

    [Fact]
    public void UpdateContact_DifferentPhoneTypes_ShouldUpdatePhone()
    {
        var contact = ContactBuilder.Valid();

        foreach (var type in Enum.GetValues<PhoneType>())
        {
            var phone = Phone.CreatePhone("11", "987654321", type);
            contact.UpdateContact(null, null, phone, null, null);
            contact.Phone.PhoneType.Should().Be(type);
        }
    }

    // ── FirstName failures ────────────────────────────────────────────────────

    [Fact]
    public void UpdateContact_EmptyFirstName_ShouldThrowDomainException()
    {
        var contact = ContactBuilder.Valid();

        var act = () => contact.UpdateContact("", null, null, null, null);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*First name*");
    }

    [Fact]
    public void UpdateContact_WhitespaceOnlyFirstName_ShouldThrowDomainException()
    {
        var contact = ContactBuilder.Valid();

        var act = () => contact.UpdateContact("   ", null, null, null, null);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*First name*");
    }

    [Fact]
    public void UpdateContact_EmptyFirstName_ShouldNotMutateFirstName()
    {
        var contact = ContactBuilder.Valid();
        var original = contact.FirstName;

        try { contact.UpdateContact("", null, null, null, null); } catch { }

        contact.FirstName.Should().Be(original);
    }

    // ── Email failures ────────────────────────────────────────────────────────

    [Theory]
    [InlineData("bademail")]
    [InlineData("missing@")]
    [InlineData("@domain.com")]
    [InlineData("two@@domain.com")]
    [InlineData("no-tld@domain")]
    public void UpdateContact_InvalidEmailFormat_ShouldThrowDomainException(string invalidEmail)
    {
        var contact = ContactBuilder.Valid();

        var act = () => contact.UpdateContact(null, null, null, invalidEmail, null);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*email*");
    }

    [Fact]
    public void UpdateContact_InvalidEmail_ShouldNotMutateEmail()
    {
        var contact = ContactBuilder.Valid();
        var original = contact.Email;

        try { contact.UpdateContact(null, null, null, "bad-email", null); } catch { }

        contact.Email.Should().Be(original);
    }

    // ── BirthDate failures ────────────────────────────────────────────────────

    [Fact]
    public void UpdateContact_FutureBirthDate_ShouldThrowDomainException()
    {
        var contact = ContactBuilder.Valid();
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

        var act = () => contact.UpdateContact(null, null, null, null, tomorrow);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*Birth date*future*");
    }

    [Fact]
    public void UpdateContact_FarFutureBirthDate_ShouldThrowDomainException()
    {
        var contact = ContactBuilder.Valid();
        var farFuture = new DateOnly(2999, 1, 1);

        var act = () => contact.UpdateContact(null, null, null, null, farFuture);

        act.Should().Throw<DomainExceptions>()
            .WithMessage("*Birth date*future*");
    }

    [Fact]
    public void UpdateContact_FutureBirthDate_ShouldNotMutateBirthDate()
    {
        var contact = ContactBuilder.Valid();
        var original = contact.BirthDate;
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

        try { contact.UpdateContact(null, null, null, null, tomorrow); } catch { }

        contact.BirthDate.Should().Be(original);
    }

    // ── State integrity after multiple failures ────────────────────────────────

    [Fact]
    public void UpdateContact_MultipleSequentialFailures_ShouldLeaveContactUnchanged()
    {
        var contact = ContactBuilder.Valid();
        var snapshot = new
        {
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.BirthDate
        };

        try { contact.UpdateContact("", null, null, null, null); } catch { }
        try { contact.UpdateContact(null, null, null, "invalid", null); } catch { }
        try { contact.UpdateContact(null, null, null, null, new DateOnly(2999, 1, 1)); } catch { }

        contact.FirstName.Should().Be(snapshot.FirstName);
        contact.LastName.Should().Be(snapshot.LastName);
        contact.Email.Should().Be(snapshot.Email);
        contact.BirthDate.Should().Be(snapshot.BirthDate);
    }

    [Fact]
    public void UpdateContact_ValidUpdateAfterFailedUpdate_ShouldSucceed()
    {
        var contact = ContactBuilder.Valid();

        try { contact.UpdateContact("", null, null, null, null); } catch { }

        contact.UpdateContact("ValidName", null, null, null, null);

        contact.FirstName.Should().Be("ValidName");
    }
}