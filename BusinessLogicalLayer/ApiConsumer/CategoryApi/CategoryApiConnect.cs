using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using Entities;
using Newtonsoft.Json;
using Shared.Responses;

namespace BusinessLogicalLayer.ApiConsumer.CategoryApi
{
    public class CategoryApiConnect : ICategoryApiConnect
    {
        Uri baseAddress = new Uri("https://kitsu.io/api/edge/");

        private readonly IMangaService _mangaService;
        private readonly IMangaDAL _mangaDAL;
        public CategoryApiConnect(IMangaService mangaService, IMangaDAL mangaDAL)
        {
            this._mangaDAL = mangaDAL;
            this._mangaService = mangaService;
        }
        public async Task CovertiCatego()
        {
            int LimitesCategory = 246;
            int Last = await _mangaDAL.GetLastIndexCategory();
            if (LimitesCategory == Last)
            {
                return;
            }
            Last++;
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                for (int i = Last; i <= LimitesCategory; i++)
                {
                    using (var response = await httpClient.GetAsync($"categories/{i}"))
                    {

                        string jsonString = await response.Content.ReadAsStringAsync();
                        if (jsonString.Contains("errors"))
                        {
                        }
                        else
                        {
                            RootCate? mangaRootDTO = JsonConvert.DeserializeObject<RootCate>(jsonString);
                            //Ou pegar em lista ou convert um por um pois ta fazendo lista de um so sempre
                            Category c = Convertercate.CovertiCatego(mangaRootDTO);
                            //BLL
                            Response responseManga = await _mangaService.InsertCategory(c);
                        }
                    }
                }
                return;
            }
        }
    }
}
