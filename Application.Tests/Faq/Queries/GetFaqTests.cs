using Application.Common.Interfaces.Repositories;
using Application.Faq.Queries.GetFaq;
using Domain.Entities;
using Moq;

public class GetFaqTests
{
    private readonly Mock<IFaqRepository> _faqRepositoryMock;
    private readonly GetFaqQueryHandler _handler;

    public GetFaqTests()
    {
        _faqRepositoryMock = new Mock<IFaqRepository>();
        _handler = new GetFaqQueryHandler(_faqRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedFaqResults()
    {
        // Arrange
        var fakeFaqs = new Faq[]
        {
            new Faq { Question = "What is xUnit?", Answer = "A testing framework.", Language = "fr" },
            new Faq { Question = "What is Moq?", Answer = "A mocking library.", Language = "fr" }
        };

        _faqRepositoryMock
        .Setup(repo => repo.GetByLangAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(fakeFaqs);

        var query = new GetFaqQuery() { Lang = "fr" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.Equal(2, result.Result.Length);
        Assert.Equal("What is xUnit?", result.Result[0].Question);
        Assert.Equal("A testing framework.", result.Result[0].Answer);
        Assert.Equal("What is Moq?", result.Result[1].Question);
        Assert.Equal("A mocking library.", result.Result[1].Answer);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyArray_WhenNoFaqsExist()
    {
        // Arrange
        _faqRepositoryMock
            .Setup(repo => repo.GetByLangAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var query = new GetFaqQuery() { Lang = "fr" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Result);
    }
}
