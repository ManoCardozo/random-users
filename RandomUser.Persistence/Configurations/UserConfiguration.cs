using RandomUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RandomUser.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.UserId);

            builder
                .Property(p => p.Title)
                .HasMaxLength(20);

            builder
                .Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.DateOfBirth)
                .IsRequired();

            builder
                .Property(p => p.PhoneNumber)
                .HasMaxLength(50);

            builder
                .Property(p => p.ProfileImage)
                .HasColumnType("image");

            builder
                .Property(p => p.ProfileThumbnail)
                .HasColumnType("image");
        }
    }
}
