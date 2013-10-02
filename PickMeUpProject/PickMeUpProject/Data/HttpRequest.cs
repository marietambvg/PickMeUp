using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace PickMeUpProject.Data
{
    public class HttpRequest
    {
        public static async Task<T> Get<T>(string resourceUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, resourceUrl);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(resourceUrl);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseText=await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<T>(responseText);
            return responseData;
        }
       
    }
}