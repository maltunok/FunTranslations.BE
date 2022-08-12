using FunTranslations.ApiClient.Models;

namespace FunTranslations.Test.Mock;

public class TranslationRequestMockData
{
    public static FunTranslationsRequestModel GetRequestAsync()
    {
        return new FunTranslationsRequestModel
        {
           ApiType = "leetspeak",
           Text = "cool project"
        };
    }

}
