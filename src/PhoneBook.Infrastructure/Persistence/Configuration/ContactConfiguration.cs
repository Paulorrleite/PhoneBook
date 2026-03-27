using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Domain;

namespace PhoneBook.Infrastructure.Persistence.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Phone, phone =>
        {
            phone.Property(p => p.DDD)
                .IsRequired()
                .HasMaxLength(5);
            phone.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.Property(x => x.Email)
            .HasMaxLength(250);

        builder.Property(x => x.BirthDate)
            .IsRequired();
    }
}
