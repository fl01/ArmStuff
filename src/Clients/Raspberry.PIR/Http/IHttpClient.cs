using System.Net.Http;

namespace Raspberry.PIR.Http
{
    public interface IHttpClient
    {
        HttpResponseMessage PostJson<T>(string url, T data);
    }
}
