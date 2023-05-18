using BusinessLogicalLayer.ApiConsumer.MangaApi.MangaCategoryApi;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.ApiConsumer.CategoryToItemApi
{
    public class CategoryToAnime
    {
        public static async Task<List<Category>> AnimeCategory(int Id)
        {
            Uri baseAddress = new Uri("https://kitsu.io/api/edge/anime/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var response = await httpClient.GetAsync($"{Id}/relationships/categories"))
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    if (jsonString.Contains("errors"))
                    {
                    }
                    else
                    {
                        RootMA? mangaRootDTO = JsonConvert.DeserializeObject<RootMA>(jsonString);
                        //Ou pegar em lista ou convert um por um pois ta fazendo lista de um so sempre
                        List<Category> CateReturn = ConverterCategoryToItem.CovertiMangaCate(mangaRootDTO);
                        //BLL
                        return CateReturn;
                    }
                }
            }
            return null;
        }
    }
}
