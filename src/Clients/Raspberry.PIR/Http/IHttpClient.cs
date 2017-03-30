using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Raspberry.PIR.Http
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, IEnumerable<KeyValuePair<string, string>> headers = null);
    }
}
