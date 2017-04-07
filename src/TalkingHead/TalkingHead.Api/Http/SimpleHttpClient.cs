using System.Net.Http;
using System.Threading.Tasks;

namespace TalkingHead.Api.Http
{
    public class SimpleHttpClient : IHttpClient
    {
        private HttpClient _httpClient;

        public SimpleHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<byte[]> GetContentAsByte(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            string content = await response?.Content?.ReadAsStringAsync();
            return await response?.Content?.ReadAsByteArrayAsync();
        }
    }
}
