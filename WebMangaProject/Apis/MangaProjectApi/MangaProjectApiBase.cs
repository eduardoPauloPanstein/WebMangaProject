using System.Net.Http.Headers;

namespace MvcPresentationLayer.Apis.MangaProjectApi
{
    public abstract class MangaProjectApiBase
    {
        public HttpClient client = new();

        //socket exaustation - HttpClient -> DI

        public MangaProjectApiBase()
        {
            client.BaseAddress = new Uri("https://animalistwebapiservice.azurewebsites.net/api/");   //https://animalistwebapiservice.azurewebsites.net/api/ //https://localhost:7164/api/

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ////First get user claims    
            //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            ////Filter specific claim    
            //var token = (claims?.FirstOrDefault(x => x.Type.Equals("Token", StringComparison.OrdinalIgnoreCase))?.Value);

            //if (token != null)
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}

        }
    }
}
