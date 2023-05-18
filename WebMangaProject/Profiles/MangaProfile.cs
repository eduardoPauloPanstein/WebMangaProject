using AutoMapper;
using Entities.MangaS;
using MvcPresentationLayer.Models.MangaModels;
using Shared.Models.Manga;

namespace MvcPresentationLayer.Profiles
{
    public class MangaProfile : Profile
    {
        public MangaProfile()
        {
            CreateMap<MangaShortViewModel, MangaCatalog>();
            CreateMap<MangaCatalog, MangaShortViewModel>();


            CreateMap<MangaShortViewModel, Manga>();
            CreateMap<Manga, MangaShortViewModel>();

            CreateMap<MangaShortDbViewModel, Manga>();
            CreateMap<Manga, MangaShortDbViewModel>();

            CreateMap<MangaOnPageViewModel, Manga>();
            CreateMap<Manga, MangaOnPageViewModel>();

            CreateMap<MangaComentaryViewModel, MangaComentary>();
            CreateMap<MangaComentary, MangaComentaryViewModel>();

            

        }
    }
}
