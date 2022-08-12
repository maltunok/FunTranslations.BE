using FluentAssertions;
using FunTranslations.ApiClient.Controllers;
using FunTranslations.ApiClient.Services.FunTranslations;
using FunTranslations.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FunTranslations.Test.System.Controllers;

public class TestTranslationController
{
    [Fact]
    public async Task TranslateAsync_ShouldReturn200Status()
    {
        //Arrange
        var translationService = new Mock<IFunTranslationsApiService>();
        ILogger<TranslationsController> logger = new Logger<TranslationsController>(new NullLoggerFactory());
        translationService.Setup(_ => _.GetTranslationsAsync("leetspeak", "cool project", new CancellationToken()))
                          .ReturnsAsync(TranslationResponseMockData.GetTranslationsAsync);
        var sut = new TranslationsController(logger, translationService.Object);

        //Act
        var result = (CreatedResult)await sut.Get(TranslationRequestMockData.GetRequestAsync(), new CancellationToken());

        //Assert
        result.StatusCode.Should().Be(201);
    }
}
