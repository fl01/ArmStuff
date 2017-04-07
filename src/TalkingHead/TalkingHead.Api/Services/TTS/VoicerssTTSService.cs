using System.Threading.Tasks;
using TalkingHead.Api.Http;

namespace TalkingHead.Api.Services.TTS
{
    public class VoicerssTTSService : ITTSService
    {
        private readonly IHttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public VoicerssTTSService(ISettingsService settingsService, IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _settingsService = settingsService;
        }

        public async Task<byte[]> TextToSpeechAsync(string text, AudioCodec codec, AudioFormat format, OutputLanguage language)
        {
            string baseAddress = _settingsService.GetVoiceRssApiUrl();
            string query = BuildQuery(text, codec, format, language);

            byte[] response = await _httpClient.GetContentAsByte(baseAddress + query);

            return response;
        }

        private string BuildQuery(string text, AudioCodec codec, AudioFormat format, OutputLanguage language)
        {
            return $"key={_settingsService.GetVoiceRssApiKey()}&hl={GetOutputLanguage(language)}&src={text}&c={GetAudioCodec(codec)}&f={GetAudioFormat(format)}";
        }

        private string GetAudioCodec(AudioCodec codec)
        {
            switch (codec)
            {
                case AudioCodec.MP3:
                default:
                    return "MP3";
            }
        }

        private string GetAudioFormat(AudioFormat format)
        {
            switch (format)
            {
                case AudioFormat._8khz_8bit_mono:
                    return "8khz_8bit_mono";
                case AudioFormat._48khz_16bit_stereo:
                default:
                    return "48khz_16bit_stereo";
            }
        }

        private string GetOutputLanguage(OutputLanguage language)
        {
            switch (language)
            {
                case OutputLanguage.EnglishUS:
                default:
                    return "en-us";
            }
        }
    }
}
