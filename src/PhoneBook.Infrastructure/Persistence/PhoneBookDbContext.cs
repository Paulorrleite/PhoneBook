using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain;

namespace PhoneBook.Infrastructure.Persistence;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Phone> Phones => Set<Phone>();
}
