using Newtonsoft.Json;
using Shared;
using Shared.Responses;
using System.Net.Http.Headers;

namespace MvcPresentationLayer.Apis.MangaProjectApi.ItemComentary.AnimeComentary
{
    public class MangaProjectApiAnimeComentary : MangaProjectApiBase, IMangaProjectApiAnimeComentary
    {
        public async Task<Response> Delete(int? id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.DeleteAsync($"AnimeComentary/{id}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Entities.AnimeS.AnimeComentary>();
                }
                return JsonConvert.DeserializeObject<Response>(await responseHttp.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<DataResponse<Entities.AnimeS.AnimeComentary>> Get(string? token, int skip = 0, int take = 25)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"AnimeComentary/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>(null);
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Entities.AnimeS.AnimeComentary>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>(ex);
            }
        }

        public async Task<SingleResponse<Entities.AnimeS.AnimeComentary>> Get(int id, string? token)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"AnimeComentary/ById/{id}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Entities.AnimeS.AnimeComentary>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<SingleResponse<Entities.AnimeS.AnimeComentary>>(data);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Entities.AnimeS.AnimeComentary>(dataResponse.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Entities.AnimeS.AnimeComentary>(ex);
            }
        }

        public async Task<DataResponse<Entities.AnimeS.AnimeComentary>> GetByAnime(int AnimeId)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"AnimeComentary/ByAnime/{AnimeId}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Entities.AnimeS.AnimeComentary>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Entities.AnimeS.AnimeComentary>(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>(ex);
            }
        }

        public async Task<DataResponse<Entities.AnimeS.AnimeComentary>> GetByUser(int userid)
        {
            try
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"AnimeComentary/ByUser/{userid}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<Entities.AnimeS.AnimeComentary>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Entities.AnimeS.AnimeComentary>(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Entities.AnimeS.AnimeComentary>(ex);
            }
        }

        public async Task<Response> Insert(Entities.AnimeS.AnimeComentary item, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string serialized = JsonConvert.SerializeObject(item);
                using HttpResponseMessage responseHttp = await client.PostAsJsonAsync("AnimeComentary", serialized);

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

        public async Task<Response> Update(Entities.AnimeS.AnimeComentary item, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string serialized = JsonConvert.SerializeObject(item);
                using HttpResponseMessage responseHttp = await client.PutAsJsonAsync($"AnimeComentary/{item.Id}", serialized);

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
    }
}
