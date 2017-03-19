﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Raspberry.PIR.Http
{
    public class SimpleHttpClient : IHttpClient
    {
        private HttpClient _httpClient;

        public SimpleHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data)
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

            return _httpClient.PostAsync(url, new StringContent(serializedData, Encoding.UTF8, "application/json"));
        }
    }
}
