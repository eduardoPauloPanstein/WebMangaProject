using AutoMapper;
using Entities.AnimeS;
using MvcPresentationLayer.Models.AnimeModel;
using Shared.Models.Anime;

namespace MvcPresentationLayer.Profiles
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            CreateMap<AnimeShortViewModel, AnimeCatalog>();
            CreateMap<AnimeCatalog, AnimeShortViewModel>();

            CreateMap<AnimeShortViewModel, Anime>();
            CreateMap<Anime, AnimeShortViewModel>();

            //CreateMap<MangaShortDbViewModel, Anime>();
            //CreateMap<Manga, MangaShortDbViewModel>();

            CreateMap<AnimeOnpageViewModel, Anime>();
            CreateMap<Anime, AnimeOnpageViewModel>();

            CreateMap<AnimeComentaryViewModel, AnimeComentary>();
            CreateMap<AnimeComentary, AnimeComentaryViewModel>();

            
        }
    }
}
