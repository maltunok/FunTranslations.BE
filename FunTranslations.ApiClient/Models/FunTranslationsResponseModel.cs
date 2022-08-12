using Newtonsoft.Json;

namespace FunTranslations.ApiClient.Models;

public class FunTranslationsResponseModel
{
    [JsonProperty("success")]
    public SuccessModel? Success { get; set; }

    [JsonProperty("contents")]
    public ContentsModel? Contents { get; set; }

    [JsonProperty("error")]
    public ErrorModel? Error { get; set; }

    public class ContentsModel
    {
        [JsonProperty("translated")]
        public string? Translated { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("translation")]
        public string? Translation { get; set; }
    }

    public class SuccessModel
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class ErrorModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
