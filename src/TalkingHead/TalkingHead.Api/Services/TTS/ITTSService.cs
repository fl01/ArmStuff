using System.Threading.Tasks;

namespace TalkingHead.Api.Services.TTS
{
    public interface ITTSService
    {
        Task<byte[]> TextToSpeechAsync(string text, AudioCodec codec, AudioFormat format, OutputLanguage language);
    }
}
