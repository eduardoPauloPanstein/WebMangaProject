using Entities.AnimeS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    public class AnimeRatingFrequenciesMapConfig : IEntityTypeConfiguration<AnimeRatingFrequencies>
    {
        public void Configure(EntityTypeBuilder<AnimeRatingFrequencies> builder)
        {
            builder.ToTable("AnimeRatingFrequencies");

        }
    }
}
