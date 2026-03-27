using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Domain;

namespace PhoneBook.Infrastructure.Persistence.Configuration;

public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable("Phones");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DDD)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.Number)
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(x => x.PhoneType)
            .IsRequired();
    }
}
