using PhoneBook.Domain;
using PhoneBook.Domain.Repositories;
using PhoneBook.Infrastructure.Persistence;

namespace PhoneBook.Infrastructure.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(PhoneBookDbContext context) : base(context)
    {
    }
}
