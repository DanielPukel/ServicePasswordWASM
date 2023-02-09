using Refit;
using ServicePassword.API.Responses;

namespace AuthentificationTEsting.WASP.Query
{
    public interface IGetServicePasswordQuery
    {
        [Get("/api/password")]
        Task<ServicePasswordResponse> Execute();
    }
}
