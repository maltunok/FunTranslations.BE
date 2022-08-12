using FunTranslations.ApiClient.Models;
using FunTranslations.ApiClient.Settings;
using FunTranslations.Infrastructure.Data;
using FunTranslations.Infrastructure.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace FunTranslations.ApiClient.Services.FunTranslations;

public class FunTranslationsApiService : IFunTranslationsApiService
{
    private readonly ILogger<FunTranslationsApiService> _logger;
    private readonly IAsyncRepository<RequestResponseRecord> _requestResponseRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiSettings _apiSettings;
    private readonly JsonSerializerSettings _settings;

    public FunTranslationsApiService(ILogger<FunTranslationsApiService> logger, 
        IAsyncRepository<RequestResponseRecord> requestResponseRepository, 
        IHttpClientFactory httpClientFactory,
        IOptions<ApiSettings> apiSettingsSnapshot)
    {
        _logger = logger;
        _requestResponseRepository = requestResponseRepository;
        _httpClientFactory = httpClientFactory;
        _apiSettings = apiSettingsSnapshot.Value;
        _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
    }

    public async Task<IReadOnlyList<RequestResponseRecord>> GetAllTranslationRecordsAsync(int perPage, int page, CancellationToken cancellationToken)
    {
        return await _requestResponseRepository.ListAllAsync(perPage, page, cancellationToken);
    }

    public async Task<FunTranslationsResponseModel> GetTranslationsAsync(string apiType, string text, CancellationToken cancellationToken)
    {
        var requestUrl = $"{_apiSettings.BaseUrl}{apiType}.json?text={text}";

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
        httpRequestMessage.Headers.Add("Accept", "application/json");

        var httpClient = _httpClientFactory.CreateClient();

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (httpResponseMessage.StatusCode == HttpStatusCode.TooManyRequests)
        {
            _logger.LogWarning("Too many requests");

            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FunTranslationsResponseModel>(response, _settings);

            var requestResponseRecord = new RequestResponseRecord
            {
                RequestUrl = requestUrl,
                ResponseCode = (int)HttpStatusCode.TooManyRequests,
                RequestedText = text,
                ResponseText = result?.Error?.Message!
            };

            await _requestResponseRepository.AddAsync(requestResponseRecord, cancellationToken);

            return result!;
        }

        else if (httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation("Request is successful");

            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<FunTranslationsResponseModel>(response, _settings);

            var requestResponseRecord = new RequestResponseRecord
            {
                RequestUrl = requestUrl,
                ResponseCode = (int)HttpStatusCode.OK,
                RequestedText = text,
                TranslatedText = result?.Contents?.Translated!,
                ResponseText = "Success"
            };

            await _requestResponseRepository.AddAsync(requestResponseRecord, cancellationToken);

            return result!;
        }
        else
        {
            return await Task.FromResult<FunTranslationsResponseModel>(null!);
        }
    }
}
