using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Newtonsoft.Json;
using Shared;
using Shared.Responses;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class ApiConnect : IApiConnect
    {
        //https://kitsu.io/api/edge/manga?page[limit]=20&page[offset]=0
        // pageLimit Max=20

        Uri baseAddress = new Uri("https://kitsu.io/api/edge/");

        private readonly IMangaService _mangaService;
        public ApiConnect(IMangaService mangaService)
        {
            this._mangaService = mangaService;
        }
        public async Task Consume()
        {
            int last = await _mangaService.GetLastIndex();
            int LimiteManga = 64595;

            if (last >= LimiteManga)
            {
                return;
            }
            last++;
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int i = last; i <= LimiteManga; i++)
                {
                    using (var response = await httpClient.GetAsync($"manga/{i}"))
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                     
                        if (jsonString.Contains("errors"))
                        {
                        }
                        else
                        {
                            
                            Root? mangaRootDTO = JsonConvert.DeserializeObject<Root>(jsonString);
                            //Ou pegar em lista ou convert um por um pois ta fazendo lista de um so sempre
                            Manga manga = ConverterToCategory.ConvertDTOToManga(mangaRootDTO);
                            manga.Genres = await CategoryToMangaApi.MangaCategory(Convert.ToInt32(manga.Id));
                            //BLL
                            Response responseManga = await _mangaService.Insert(manga);
                        }
                    }
                }
            }
            return;
        }
    }
}
