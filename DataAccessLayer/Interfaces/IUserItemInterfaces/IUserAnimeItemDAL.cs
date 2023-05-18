using Entities.AnimeS;
using Entities.UserS;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUserItem
{
    public interface IUserAnimeItemDAL : ICrudUserItem<UserAnimeItem,Anime>
    {
       
    }
}
