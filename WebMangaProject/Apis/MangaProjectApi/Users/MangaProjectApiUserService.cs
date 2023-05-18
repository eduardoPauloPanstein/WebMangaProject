using Entities.UserS;
using Newtonsoft.Json;
using Shared;
using Shared.Models.User;
using Shared.Responses;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace MvcPresentationLayer.Apis.MangaProjectApi
{
    public class MangaProjectApiUserService : MangaProjectApiBase, IMangaProjectApiUserService
    {
        public async Task<Response> Delete(int? id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.DeleteAsync($"User/{id}");

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

        public async Task<Response> Insert(UserCreate userCreate, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //string serialized = JsonConvert.SerializeObject(user);
                using HttpResponseMessage responseHttp = await client.PostAsJsonAsync("User", userCreate);

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

        public async Task<SingleResponseWToken<User>> Login(UserLogin userLogin)
        {
            try
            {
                //string serialized = JsonConvert.SerializeObject(userLogin);
                using HttpResponseMessage responseHttp = await client.PostAsJsonAsync("User/Authenticate", userLogin);

                if (responseHttp.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<SingleResponseWToken<User>>(responseHttp.Content.ReadAsStringAsync().Result);

                return ResponseFactory.CreateInstance().CreateFailedSingleResponseWToken<User>();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseWToken<User>(ex);
            }
        }

        public async Task<SingleResponse<User>> Get(int id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using HttpResponseMessage responseHttp = await client.GetAsync($"User/{id}");

                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<User>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<SingleResponse<User>>(data);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<User>(dataResponse.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<User>(ex);
            }
        }

        public async Task<DataResponse<User>> Get(string token, int skip = 0, int take = 25)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.GetAsync($"User/skip/{skip}/take/{take}");
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return ResponseFactory.CreateInstance().CreateFailedDataResponse<User>();
                }
                var data = await responseHttp.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<DataResponse<User>>(data);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(dataResponse.Data);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<User>(ex);
            }
        }

        public async Task<Response> Update(User user, string token)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Insert(User item, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Update(UserProfileUpdate userProfileUpdate, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using HttpResponseMessage responseHttp = await client.PutAsJsonAsync($"User/{userProfileUpdate.Id}", userProfileUpdate);

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
