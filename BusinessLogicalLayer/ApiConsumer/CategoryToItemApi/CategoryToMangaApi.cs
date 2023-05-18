using BusinessLogicalLayer.ApiConsumer.MangaApi.MangaCategoryApi;
using Entities;
using Newtonsoft.Json;
namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class CategoryToMangaApi
    {
        public static async Task<List<Category>> MangaCategory(int Id)
        {
            Uri baseAddress = new Uri("https://kitsu.io/api/edge/manga/");
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
