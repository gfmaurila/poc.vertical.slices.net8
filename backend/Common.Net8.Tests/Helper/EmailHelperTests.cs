using Common.Net8.Helper;

namespace Common.Net8.Tests.Helper;

public class EmailHelperTests
{
    [Fact]
    public void GeneratePasswordResetMessage_ReturnsCorrectMessage()
    {
        // Arrange
        var info = new PasswordResetInfo
        {
            UserName = "John Doe",
            ExpiryTime = TimeSpan.FromHours(24),
            ResetLink = "https://example.com/reset"
        };

        var expectedMessage =
            "Prezado(a) John Doe,\n\n" +
            "Você solicitou a redefinição da sua senha. Por favor, observe que você tem 24 horas para concluir este processo. Clique no link abaixo para criar uma nova senha:\n\n" +
            "https://example.com/reset\n\n" +
            "Caso não tenha solicitado uma redefinição de senha, por favor, desconsidere este e-mail ou entre em contato com o suporte se tiver alguma dúvida.\n\n" +
            "Atenciosamente,\n" +
            "Equipe de Suporte";

        // Act
        var message = EmailHelper.GeneratePasswordResetMessage(info);

        // Assert
        Assert.Equal(expectedMessage, message);
    }

    [Fact]
    public void GeneratePasswordResetSubject_ReturnsCorrectSubject()
    {
        // Act
        var subject = EmailHelper.GeneratePasswordResetSubject();

        // Assert
        Assert.Equal("Redefinição de Senha Solicitada", subject);
    }
}
