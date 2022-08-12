using FunTranslations.ApiClient.Models;

namespace FunTranslations.Test.Mock;

public class TranslationResponseMockData
{
    public static FunTranslationsResponseModel GetTranslationsAsync()
    {
        return new FunTranslationsResponseModel
        {
            Error = null,
            Success = null,
            Contents = new FunTranslationsResponseModel.ContentsModel
            {
                Translated = "c0O1 Pr0j3C7"
            }
        };
    }

}
