using PhoneBook.Domain;

namespace PhoneBook.DomainTests.Builder
{
    internal static class ContactBuilder
    {
        /// <summary>Returns a fully populated valid Contact.</summary>
        public static Contact Valid() =>
            Contact.CreateContact(
                firstName: "John",
                lastName: "Doe",
                phone: PhoneBuilder.Valid(),
                email: "john.doe@example.com",
                birthDate: new DateOnly(1990, 6, 15));

        /// <summary>Returns a valid Contact with no optional fields.</summary>
        public static Contact Minimal() =>
            Contact.CreateContact(
                firstName: "John",
                lastName: null,
                phone: PhoneBuilder.Valid(),
                email: null,
                birthDate: new DateOnly(1990, 6, 15));
    }
}
