using FunTranslations.ApiClient.Models;
using FunTranslations.Infrastructure.Entities;

namespace FunTranslations.ApiClient.Services.FunTranslations;

public interface IFunTranslationsApiService
{
    Task<FunTranslationsResponseModel> GetTranslationsAsync(string apiType, string text, CancellationToken cancellationToken);
    Task<IReadOnlyList<RequestResponseRecord>> GetAllTranslationRecordsAsync(int perPage, int page, CancellationToken cancellationToken);
}
