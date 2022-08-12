namespace FunTranslations.Infrastructure.Entities;
public class RequestResponseRecord : BaseEntity
{
    public string RequestUrl { get; set; } = string.Empty;
    public int ResponseCode { get; set; }
    public string RequestedText { get; set; } = string.Empty;
    public string TranslatedText { get; set; } = string.Empty;
    public string ResponseText { get; set; } = string.Empty;
}
