using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    public class MangaComentaryMapConfig : IEntityTypeConfiguration<MangaComentary>
    {
        public void Configure(EntityTypeBuilder<MangaComentary> builder)
        {
            builder.ToTable("MangaComentary");
        }
    }
}
