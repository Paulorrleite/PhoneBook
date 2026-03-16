using PhoneBook.Domain;
using PhoneBook.Domain.Repositories;
using PhoneBook.Infrastructure.Persistence;

namespace PhoneBook.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PhoneBookDbContext _context;

    public ContactRepository(PhoneBookDbContext phoneBookDbContext)
    {
        _context = phoneBookDbContext;
    }

    public async Task AddAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Update(contact);

        await _context.SaveChangesAsync();
    }
}
