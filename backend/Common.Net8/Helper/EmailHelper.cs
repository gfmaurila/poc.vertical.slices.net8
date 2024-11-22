namespace Common.Net8.Helper;

public static class EmailHelper
{
    public static string GeneratePasswordResetSubject()
    {
        return "Redefinição de Senha Solicitada";
    }
    public static string GeneratePasswordResetMessage(PasswordResetInfo info)
    {
        var expiryHours = info.ExpiryTime.TotalHours;
        return
            $"Prezado(a) {info.UserName},\n\n" +
            $"Você solicitou a redefinição da sua senha. Por favor, observe que você tem {expiryHours} horas para concluir este processo. Clique no link abaixo para criar uma nova senha:\n\n" +
            $"{info.ResetLink}\n\n" +
            "Caso não tenha solicitado uma redefinição de senha, por favor, desconsidere este e-mail ou entre em contato com o suporte se tiver alguma dúvida.\n\n" +
            "Atenciosamente,\n" +
            "Equipe de Suporte";
    }
}

