using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace DataAccessLayer.Mappings
{
    internal class UserMapConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Nickname).IsRequired().HasMaxLength(LocationConstants.Nickname.MaxLength).IsUnicode(true);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(LocationConstants.EmailMaxLength).IsUnicode(true);
            builder.Property(u => u.Password).IsRequired();
            //builder.Property(u => u.ConfirmPassword).IsRequired();
            builder.Property(u => u.LastLogin).HasColumnType("datetime2");

        }
    }
}
