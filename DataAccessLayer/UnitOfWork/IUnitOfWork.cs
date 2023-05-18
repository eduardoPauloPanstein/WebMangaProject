using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.Interfaces.IUSerInterfaces;
using DataAccessLayer.Interfaces.IUserItem;
using Shared.Responses;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserDAL UserRepository { get; }
        IMangaDAL MangaRepository { get; }
        IAnimeDAL AnimeRepository { get; }

        IUserMangaItemDAL UserMangaItemRepository { get; }
        IUserAnimeItemDAL UserAnimeItemRepository { get; }

        IMangaComentaryDAL MangaComentaryRepository { get; }
        IAnimeComentaryDAL AnimeComentaryRepository { get; }

        Task<Response> Commit();
        Task<Response> CommitForUser();
    }
}
