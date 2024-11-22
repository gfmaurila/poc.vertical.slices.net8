using Common.Net8.Interface;
using Common.Net8.Model;
using Common.Net8.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Common.Net8.Controllers;

/// <summary>
/// Controlador base que fornece funcionalidades comuns a outros controladores.
/// </summary>
[ExcludeFromCodeCoverage]
[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificationHandle _notification;

    /// <summary>
    /// Identificador do usuário.
    /// </summary>
    protected int UserId { get; set; }

    /// <summary>
    /// Indica se é um cliente do projeto ou empresa contratante.
    /// </summary>
    protected bool IsCustomer { get; set; }

    /// <summary>
    /// Indica se os dados são de um usuário ou da empresa.
    /// </summary>
    protected bool IsCompany { get; set; }

    /// <summary>
    /// Indica se a empresa é principal.
    /// </summary>
    protected bool MainCompany { get; set; }

    protected int ProductId { get; set; }
    protected int AccountId { get; set; }

    /// <summary>
    /// Indica se o usuário está autenticado.
    /// </summary>
    protected bool AuthenticatedUser { get; set; }

    /// <summary>
    /// Construtor do MainController.
    /// </summary>
    /// <param name="notification">Handler para notificações.</param>
    protected MainController(INotificationHandle notification)
    {
        _notification = notification;
        // Valores default de exemplo
        UserId = 1;
        IsCustomer = true;
        ProductId = 1;
        AccountId = 1;
        AuthenticatedUser = true;
    }

    /// <summary>
    /// Valida se existem notificações.
    /// </summary>
    protected bool IsValid()
    {
        return !_notification.IsNotification();
    }

    /// <summary>
    /// Cria uma resposta personalizada com base nas condições de validade e no status HTTP fornecido.
    /// </summary>
    /// <param name="mensagem">A mensagem a ser incluída na resposta, geralmente uma mensagem de sucesso ou de erro.</param>
    /// <param name="result">O conteúdo de dados da resposta, se aplicável.</param>
    /// <param name="statusCode">O status HTTP desejado para a resposta. Por padrão, é definido como BadRequest (400).</param>
    /// <returns>Retorna uma resposta do tipo IActionResult com base nas condições fornecidas e no status HTTP.</returns>
    protected IActionResult CustomResponse(string mensagem, object result = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (IsValid())
        {
            return Ok(new ResponseDefault
            {
                Message = mensagem,
                Success = true,
                Data = result
            });
        }

        var error = new
        {
            success = false,
            errors = _notification.GetNotification().Select(n => n.Message)
        };

        return statusCode switch
        {
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.BadRequest => BadRequest(error),
            _ => BadRequest(error)
        };
    }

    /// <summary>
    /// Cria uma resposta de autenticação com base nas condições de validade e no status HTTP fornecido.
    /// </summary>
    /// <param name="result">O conteúdo de dados da resposta, tipicamente o token ou detalhes de autenticação, se aplicável.</param>
    /// <param name="statusCode">O status HTTP desejado para a resposta. Por padrão, é definido como BadRequest (400).</param>
    /// <returns>
    /// Retorna uma resposta do tipo IActionResult com base nas condições fornecidas.
    /// Se válido e os dados de resultado estiverem presentes, retorna um status OK (200) com a mensagem de autenticado.
    /// Se válido e sem dados de resultado, retorna um status Unauthorized (401).
    /// Se inválido, retorna o status fornecido com a mensagem de erro associada.
    /// </returns>
    protected IActionResult Auth(object result = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (IsValid())
        {
            if (result != null)
            {
                return Ok(new ResponseAuth
                {
                    Data = result
                });
            }
            else
            {
                return StatusCode(401, Responses.UnauthorizedErrorMessage());
            }
        }

        var error = new
        {
            success = false,
            errors = _notification.GetNotification().Select(n => n.Message)
        };

        return statusCode switch
        {
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.BadRequest => BadRequest(error),
            _ => BadRequest(error)
        };
    }

    /// <summary>
    /// Cria uma resposta personalizada com base nas condições de validade e no status HTTP fornecido.
    /// </summary>
    /// <param name="result">O conteúdo de dados da resposta. Por padrão, é definido como null.</param>
    /// <param name="statusCode">O status HTTP desejado para a resposta. Por padrão, é definido como BadRequest (400).</param>
    /// <returns>
    /// Retorna uma resposta do tipo IActionResult com base nas condições fornecidas.
    /// Se válido, retorna um status OK (200) com o conteúdo de dados fornecido.
    /// Se inválido, retorna o status fornecido com a mensagem de erro associada.
    /// </returns>
    protected IActionResult CustomResponse(object result = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (IsValid())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        var error = new
        {
            success = false,
            errors = _notification.GetNotification().Select(n => n.Message)
        };

        return statusCode switch
        {
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.BadRequest => BadRequest(error),
            _ => BadRequest(error)
        };
    }

    /// <summary>
    /// Cria uma resposta personalizada com base na validade do estado do modelo fornecido.
    /// </summary>
    /// <param name="modelState">O estado do modelo para verificar sua validade.</param>
    /// <returns>
    /// Retorna uma resposta do tipo IActionResult.
    /// Se o estado do modelo não for válido, ele notificará os erros associados.
    /// Em seGuida, chama o método CustomResponse() para obter a resposta adequada com base na validade.
    /// </returns>
    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyInvalidModelError(modelState);
        return CustomResponse();
    }

    /// <summary>
    /// Notifica erros associados a um estado de modelo inválido.
    /// </summary>
    /// <param name="modelState">O estado do modelo para extrair os erros.</param>
    /// <remarks>
    /// Este método percorre todos os erros no estado do modelo fornecido.
    /// Se um erro estiver associado a uma exceção, ele usará a mensagem da exceção.
    /// Caso contrário, usará a mensagem de erro padrão fornecida pelo estado do modelo.
    /// Cada erro é então passado para o método NotifyError para ser tratado.
    /// </remarks>
    protected void NotifyInvalidModelError(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    /// <summary>
    /// Envia uma notificação de erro.
    /// </summary>
    /// <param name="message">Mensagem de erro a ser notificada.</param>
    /// <remarks>
    /// Este método cria uma nova instância de Notification com a mensagem fornecida
    /// e então passa essa notificação para o manipulador de notificações para processamento.
    /// </remarks>
    protected void NotifyError(string message)
    {
        _notification.Handle(new Notification(message));
    }
}
