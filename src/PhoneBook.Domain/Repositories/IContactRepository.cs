namespace PhoneBook.Domain.Repositories;

public interface IContactRepository
{
    Task AddAsync(Contact contact);

    Task UpdateAsync(Contact contact);

    Task DeleteAsync(Contact contact);
}
