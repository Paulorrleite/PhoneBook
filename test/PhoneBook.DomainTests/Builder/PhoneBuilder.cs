using PhoneBook.Domain;
using PhoneBook.Domain.Enums;

namespace PhoneBook.DomainTests.Builder
{
    internal static class PhoneBuilder
    {
        /// <summary>Returns a valid Phone using safe defaults.</summary>
        public static Phone Valid(
            string ddd = "11",
            string number = "987654321",
            PhoneType type = PhoneType.Personal) =>
                Phone.CreatePhone(ddd, number, type);
    }
}
