using Entities.AnimeS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    public class AnimeComentaryMapConfig : IEntityTypeConfiguration<AnimeComentary>
    {
        public void Configure(EntityTypeBuilder<AnimeComentary> builder)
        {
            builder.ToTable("AnimeComentary");
        }
    }
}
