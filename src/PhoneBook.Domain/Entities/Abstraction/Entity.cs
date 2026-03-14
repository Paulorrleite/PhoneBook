namespace PhoneBook.Domain.Entities.Abstraction;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
}
