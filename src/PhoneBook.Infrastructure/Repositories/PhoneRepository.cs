using PhoneBook.Domain;
using PhoneBook.Infrastructure.Persistence;

namespace PhoneBook.Infrastructure.Repositories;

public class PhoneRepository : BaseRepository<Phone>
{
    public PhoneRepository(PhoneBookDbContext context) : base(context)
    {
    }
}
