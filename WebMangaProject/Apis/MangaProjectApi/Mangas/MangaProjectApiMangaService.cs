using Entities.MangaS;
using Newtonsoft.Json;
using Shared;
using Shared.Models.Manga;
using Shared.Responses;
using System.Net.Http.Headers;

namespace MvcPresentationLayer.Apis.MangaProjectApi.Mangas
{
    public class MangaProjectApiMangaService : MangaProjectApiBase, IMangaProjectApiMangaService
    {
        public async Task<Response> Delete(int? id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.DeleteAsync($"Manga/{id}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedResponse();
                }
                return JsonConvert.DeserializeObject<Response>(await responseHttp.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Insert(Manga item, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string serialized = JsonConvert.SerializeObject(item);
                using HttpResponseMessage responseHttp = await client.PostAsJsonAsync("Manga", serialized);

                var response = JsonConvert.DeserializeObject<Response>(responseHttp.Content.ReadAsStringAsync().Result);

                if (responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateSuccessResponse();
                }
                return ResponseFactory.CreateInstance().CreateFailedResponse(response.Message);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<DataResponse<Manga>> Get(string title)
        {
            try
            {
                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByName/{title}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Manga>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Manga>(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }

        public async Task<SingleResponse<Manga>> Get(int id, string? token)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ById/{id}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<SingleResponse<Manga>>(data);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Manga>(dataResponse.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>(ex);
            }
        }

        public async Task<DataResponse<Manga>> Get(string? token, int skip = 0, int take = 25)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Manga>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetByFavorites(int skip = 0, int take = 25)
        {
            try
            {
                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByFavorites/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<MangaCatalog>>(data);

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetByUserCount(int skip = 0, int take = 25)
        {
            try
            {
                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByUserCount/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<MangaCatalog>>(data);

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<Response> Update(Manga item, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string serialized = JsonConvert.SerializeObject(item);
                using HttpResponseMessage responseHttp = await client.PutAsJsonAsync($"Manga/{item.Id}", serialized);

                var response = JsonConvert.DeserializeObject<Response>(responseHttp.Content.ReadAsStringAsync().Result);

                if (responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateSuccessResponse();
                }
                return ResponseFactory.CreateInstance().CreateFailedResponse(response.Message);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetByRating(int skip, int take)
        {
            try
            {

                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByRating/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<MangaCatalog>>(data);

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetByPopularity(int skip, int take)
        {

            try
            {

                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByPopularity/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<MangaCatalog>>(data);

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<Manga>> GetByCategory(int ID)
        {
            try
            {

                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/ByCategory/{ID}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Manga>>(data);

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }

        public async Task<SingleResponse<Manga>> GetComplete(int ID)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"Manga/MangaOnPage/{ID}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<SingleResponse<Manga>>(data);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(dataResponse.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>(ex);
            }
        }

        public Task<int> GetLastIndexCategory()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetLastIndex()
        {
            throw new NotImplementedException();
        }
    }
}
