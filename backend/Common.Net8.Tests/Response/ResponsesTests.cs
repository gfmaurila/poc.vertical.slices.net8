using Common.Net8.Response;

namespace Common.Net8.Tests.Response;

public class ResponsesTests
{
    [Fact]
    public void ApplicationErrorMessage_ReturnsCorrectResponse()
    {
        // Act
        var response = Responses.ApplicationErrorMessage();

        // Assert
        Assert.Equal("Ocorreu algum erro interno na aplicação, por favor tente novamente.", response.Message);
        Assert.False(response.Success);
        Assert.Null(response.Data);
    }

    [Fact]
    public void DomainErrorMessage_WithMessage_ReturnsCorrectResponse()
    {
        // Arrange
        var message = "Erro específico do domínio";

        // Act
        var response = Responses.DomainErrorMessage(message);

        // Assert
        Assert.Equal(message, response.Message);
        Assert.False(response.Success);
        Assert.Null(response.Data);
    }

    [Fact]
    public void DomainErrorMessage_WithErrors_ReturnsCorrectResponse()
    {
        // Arrange
        var message = "Erro com detalhes";
        var errors = new List<string> { "Detalhe1", "Detalhe2" };

        // Act
        var response = Responses.DomainErrorMessage(message, errors);

        // Assert
        Assert.Equal(message, response.Message);
        Assert.False(response.Success);
        Assert.Equal(errors, response.Data);
    }

    [Fact]
    public void UnauthorizedErrorMessage_ReturnsCorrectResponse()
    {
        // Act
        var response = Responses.UnauthorizedErrorMessage();

        // Assert
        Assert.Equal("A combinação de login e senha está incorreta!", response.Message);
        Assert.False(response.Success);
        Assert.Null(response.Data);
    }
}
