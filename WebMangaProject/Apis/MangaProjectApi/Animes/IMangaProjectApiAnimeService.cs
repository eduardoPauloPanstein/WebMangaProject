using Entities.AnimeS;
using Shared.Interfaces;
using Shared.Models.Anime;
using Shared.Responses;

namespace MvcPresentationLayer.Apis.MangaProjectApi.Animes
{
    public interface IMangaProjectApiAnimeService : IMangaProjectApiService<Anime>, IUsualGetInterfaces<AnimeCatalog,Anime>
    {

    }
}
