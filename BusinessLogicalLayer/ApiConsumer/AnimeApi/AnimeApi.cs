using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryToItemApi;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Newtonsoft.Json;
using Shared.Responses;
using System;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeApi : IAnimeApiConnect
    {
        Uri baseAddress = new Uri("https://kitsu.io/api/edge/");

        private readonly IAnimeService _AnimeService;
        public AnimeApi(IAnimeService AnimeService)
        {
            this._AnimeService = AnimeService;
        }
        public async Task ConsumeAnime()
        {
            int last = await _AnimeService.GetLastIndex();
            int LimiteAnimes = 50000;

            if (last >= LimiteAnimes)
            {
                return;
            }
            last++;
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int i = last; i <= LimiteAnimes; i++)
                {
                    if (i == 20000)
                    {
                    }
                    else if(i == 30000)
                    {
                    }
                    using (var response = await httpClient.GetAsync($"anime/{i}"))
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                       
                        if (jsonString.Contains("errors"))
                        {
                        }
                        else
                        {
                            RootANI? mangaRootDTO = JsonConvert.DeserializeObject<RootANI>(jsonString);
                            Anime anime = AnimeConverter.ConvertDTOToAnime(mangaRootDTO);
                            anime.Categories = await CategoryToAnime.AnimeCategory(Convert.ToInt32(anime.Id));
                            ////BLL
                            Response responseManga = await _AnimeService.Insert(anime);
                        }
                    }
                }
            }
            return;
        }
    }
}
