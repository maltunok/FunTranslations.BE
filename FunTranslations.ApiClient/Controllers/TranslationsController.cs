using FunTranslations.ApiClient.Models;
using FunTranslations.ApiClient.Services.FunTranslations;
using FunTranslations.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FunTranslations.ApiClient.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TranslationsController : ControllerBase
{
    private readonly ILogger<TranslationsController> _logger;
    private readonly IFunTranslationsApiService _funTranslationsApiService;

    public TranslationsController(ILogger<TranslationsController> logger, IFunTranslationsApiService funTranslationsApiService)
    {
        _logger = logger;
        _funTranslationsApiService = funTranslationsApiService;
    }

    [HttpPost, Produces("application/json")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
    [ProducesResponseType(typeof(FunTranslationsResponseModel), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Get([FromBody] FunTranslationsRequestModel funTranslationsRequestModel, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Returning Translation from api/Translations");

        return Created("api/Translations", 
            await _funTranslationsApiService.GetTranslationsAsync(funTranslationsRequestModel.ApiType, funTranslationsRequestModel.Text, cancellationToken));
    }

    [HttpGet, Produces("application/json")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
    [ProducesResponseType(typeof(IReadOnlyList<RequestResponseRecord>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllTranslationRecords(CancellationToken cancellationToken, int perPage = 10, int page = 1)
    {
        _logger.LogInformation("Returning Translation from api/Translations");

        return Ok( await _funTranslationsApiService.GetAllTranslationRecordsAsync(perPage, page,  cancellationToken));
    }
}
