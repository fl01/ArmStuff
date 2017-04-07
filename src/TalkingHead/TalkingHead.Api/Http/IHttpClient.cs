using System.Threading.Tasks;

namespace TalkingHead.Api.Http
{
    public interface IHttpClient
    {
        Task<byte[]> GetContentAsByte(string url);
    }
}
