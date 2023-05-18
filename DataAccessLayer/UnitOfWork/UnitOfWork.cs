using DataAccessLayer.ErrorHandling;
using DataAccessLayer.Implementations;
using DataAccessLayer.Implementations.UserComentaryDAL;
using DataAccessLayer.Implementations.UserItemDAL;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.Interfaces.IUSerInterfaces;
using DataAccessLayer.Interfaces.IUserItem;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MangaProjectDbContext _dbContext;
        private IUserDAL userRepository = null;
        private IMangaDAL mangaRepository = null;
        private IAnimeDAL animeRepository = null;
        private IUserMangaItemDAL userMangaItemRepository = null;
        private IUserAnimeItemDAL userAnimeItemRepository = null;
        private IMangaComentaryDAL mangaComentaryRepository = null;
        private IAnimeComentaryDAL animeComentaryRepository = null;




        public UnitOfWork(MangaProjectDbContext dbContext, IUserDAL userDAL)
        {
            this._dbContext = dbContext;
        }

        public async Task<Response> Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> CommitForUser()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return UserDbFailed.Handle(ex);
            }
        }


        private bool disposed = false;

        public IUserDAL UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserDAL(_dbContext);
                }
                return userRepository;
            }
        }

        public IMangaDAL MangaRepository
        {
            get
            {
                if (mangaRepository == null)
                {
                    mangaRepository = new MangaDAL(_dbContext);
                }
                return mangaRepository;
            }
        }

        public IAnimeDAL AnimeRepository
        {
            get
            {
                if (animeRepository == null)
                {
                    animeRepository = new AnimeDAL(_dbContext);
                }
                return animeRepository;
            }
        }

        public IUserMangaItemDAL UserMangaItemRepository
        {
            get
            {
                if (userMangaItemRepository == null)
                {
                    userMangaItemRepository = new UserMangaItemDAL(_dbContext);
                }
                return userMangaItemRepository;
            }
        }

        public IUserAnimeItemDAL UserAnimeItemRepository
        {
            get
            {
                if (userAnimeItemRepository == null)
                {
                    userAnimeItemRepository = new UserAnimeItemDAL(_dbContext);
                }
                return userAnimeItemRepository;
            }
        }

        public IMangaComentaryDAL MangaComentaryRepository
        {
            get
            {
                if (mangaComentaryRepository == null)
                {
                    mangaComentaryRepository = new MangaComentaryDAL(_dbContext);
                }
                return mangaComentaryRepository;
            }
        }

        public IAnimeComentaryDAL AnimeComentaryRepository
        {
            get
            {
                if (animeComentaryRepository == null)
                {
                    animeComentaryRepository = new AnimeComentaryDAL(_dbContext);
                }
                return animeComentaryRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
