using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kaizenplus.Domain.Users;

namespace kaizenplus.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(i => i.Username)
                .IsRequired().HasMaxLength(255);

            builder.Property(i => i.PhoneNumber)
                .IsRequired().HasMaxLength(255);

            builder.Property(i => i.PasswordHash)
                .IsRequired();

            builder.Property(i => i.PasswordSalt)
                .IsRequired();

          
        }
    }
}