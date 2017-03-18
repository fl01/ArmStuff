using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Raspberry.PIR.Http
{
    public class SimpleHttpClient : IHttpClient
    {
        public HttpResponseMessage PostJson<T>(string url, T data)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            string serializedData = JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(serializedData))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                return client.PostAsync(url, new StringContent(serializedData, Encoding.UTF8, "application/json"))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
