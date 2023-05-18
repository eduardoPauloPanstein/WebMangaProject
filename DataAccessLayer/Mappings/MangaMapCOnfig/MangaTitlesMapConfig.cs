using Microsoft.EntityFrameworkCore;
using Entities.MangaS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Mappings
{
    internal class MangaTitlesMapConfig : IEntityTypeConfiguration<MangaTitles>
    {
        public void Configure(EntityTypeBuilder<MangaTitles> builder)
        {
            builder.ToTable("MangaTitles");
        }
    }
}
