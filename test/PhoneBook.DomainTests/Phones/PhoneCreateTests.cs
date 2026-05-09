using FluentAssertions;
using PhoneBook.Domain;
using PhoneBook.Domain.Enums;
using PhoneBook.Domain.Exceptions;

namespace PhoneBook.DomainTests.Phones;

public class PhoneCreateTests
{
    // ── Success ───────────────────────────────────────────────────────────────

    [Fact]
    public void CreatePhone_ValidData_ShouldReturnPhone()
    {
        var phone = Phone.CreatePhone("11", "987654321", PhoneType.Personal);

        phone.Should().NotBeNull();
        phone.DDD.Should().Be("11");
        phone.Number.Should().Be("987654321");
        phone.PhoneType.Should().Be(PhoneType.Personal);
    }

    [Theory]
    [InlineData(PhoneType.Residential)]
    [InlineData(PhoneType.Personal)]
    [InlineData(PhoneType.Professional)]
    public void CreatePhone_AllPhoneTypes_ShouldReturnPhone(PhoneType phoneType)
    {
        var phone = Phone.CreatePhone("21", "987654321", phoneType);

        phone.Should().NotBeNull();
        phone.PhoneType.Should().Be(phoneType);
    }

    [Fact]
    public void CreatePhone_NumberWithLeadingZeros_ShouldPreserveNumber()
    {
        // string Number must preserve leading zeros — would be lost with int
        var phone = Phone.CreatePhone("11", "012345678", PhoneType.Residential);

        phone.Number.Should().Be("012345678");
    }

    // ── DDD failures ──────────────────────────────────────────────────────────

    [Theory]
    [InlineData("")]        // empty
    [InlineData("   ")]     // whitespace
    [InlineData("AA")]      // non-numeric
    [InlineData("0A")]      // mixed
    public void CreatePhone_InvalidDDD_ShouldThrowDomainException(string invalidDdd)
    {
        var act = () => Phone.CreatePhone(invalidDdd, "987654321", PhoneType.Personal);

        act.Should().Throw<DomainExceptions>();
    }

    [Fact]
    public void CreatePhone_NullDDD_ShouldThrowDomainException()
    {
        var act = () => Phone.CreatePhone(null!, "987654321", PhoneType.Personal);

        act.Should().Throw<DomainExceptions>();
    }

    // ── Number failures ───────────────────────────────────────────────────────

    [Theory]
    [InlineData("")]        // empty
    [InlineData("   ")]     // whitespace
    [InlineData("1")]     // too short
    [InlineData("ABCDEFGH")] // non-numeric
    public void CreatePhone_InvalidNumber_ShouldThrowDomainException(string invalidNumber)
    {
        var act = () => Phone.CreatePhone("11", invalidNumber, PhoneType.Personal);

        act.Should().Throw<DomainExceptions>();
    }

    [Fact]
    public void CreatePhone_NullNumber_ShouldThrowDomainException()
    {
        var act = () => Phone.CreatePhone("11", null!, PhoneType.Personal);

        act.Should().Throw<DomainExceptions>();
    }
}
