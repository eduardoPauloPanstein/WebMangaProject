using Entities;
using Entities.AnimeS;
using Entities.MangaS;
using Entities.UserS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Responses;
using System.Reflection;

namespace DataAccessLayer
{
    public class MangaProjectDbContext : DbContext
    {
        //SQL para a tabela a partir dessa propriedade.
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserMangaItem> UserManga { get; set; }
        public DbSet<UserAnimeItem> UserAnime { get; set; }

        public DbSet<RatingFrequencies> MangaRating { get; set; }
        public DbSet<AnimeRatingFrequencies> AnimeRating { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AnimeComentary> AnimeComentaries { get; set; }
        public DbSet<MangaComentary> MangaComentaries { get; set; }

        public MangaProjectDbContext(DbContextOptions<MangaProjectDbContext> options) : base(options) { }
        public MangaProjectDbContext()
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Definição de connection string e connection resiliance (se a conexão cair, tenta se reconectar até 5 vezes)
        //    optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\paulo\Documents\MangaProjectDB.mdf;Integrated Security=True;Connect Timeout=30", options => options.EnableRetryOnFailure(5));
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Category>().Property(c => c.ID).ValueGeneratedNever();
            modelBuilder.Entity<Anime>().Property(c => c.Id).ValueGeneratedNever();

            modelBuilder.Entity<AnimeRatingFrequencies>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<RatingFrequencies>().Property(c => c.Id).ValueGeneratedNever();

            //Assembly no contexto do .NET
            //Carrega os map config que tão criado dentro do projeto (assembly) DAL
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
