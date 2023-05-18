using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    internal class UserAnimeItemMapConfig : IEntityTypeConfiguration<UserAnimeItem>
    {
        public void Configure(EntityTypeBuilder<UserAnimeItem> builder)
        {
            builder.ToTable("UserAnimeItem");
            builder.Property(m => m.StartDate).HasColumnType("datetime2");
            builder.Property(m => m.FinishDate).HasColumnType("datetime2");
            builder.HasKey(m => m.Id);

            builder.HasOne(c => c.User).WithMany(c => c.AnimeList).HasConstraintName("fk_Animeuser").HasForeignKey(c => c.UserId);
        }
    }
}