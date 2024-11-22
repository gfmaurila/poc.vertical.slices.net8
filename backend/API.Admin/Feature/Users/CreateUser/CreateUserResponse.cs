using Common.Net8;

namespace API.Admin.Feature.Users.CreateUser;

public class CreateUserResponse : BaseResponse
{
    public CreateUserResponse(Guid id) => Id = id;

    public Guid Id { get; }
}



//using Common.Net8;

//namespace API.Admin.Feature.Users.CreateUser;

//public class CreateUserResponse : BaseResponse
//{
//    // Construtor para sucesso com retorno de ID
//    public CreateUserResponse(Guid id)
//    {
//        Id = id;
//        Success = true; // Como houve sucesso, define automaticamente
//        SuccessMessage = "Cadastrado com sucesso!";
//    }

//    // Construtor para resposta de erro
//    public CreateUserResponse(bool success, string successMessage, List<string> errors)
//    {
//        Success = success;
//        SuccessMessage = successMessage;
//        Errors = errors;
//    }

//    public Guid Id { get; private set; } // Use private set para que o valor só possa ser definido na construção
//    public bool Success { get; private set; }
//    public string SuccessMessage { get; private set; }
//    public List<string> Errors { get; private set; } = new List<string>();

//    // Método estático para criar uma resposta de sucesso padrão
//    public static CreateUserResponse CreateSuccessResponse(Guid id)
//    {
//        return new CreateUserResponse(id);
//    }

//    // Método estático para criar uma resposta de erro
//    public static CreateUserResponse CreateErrorResponse(List<string> errors)
//    {
//        return new CreateUserResponse(false, null, errors);
//    }
//}
