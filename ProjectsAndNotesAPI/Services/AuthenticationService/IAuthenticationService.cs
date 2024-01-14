using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(Login loginModel);
        Task<bool> RegisterAsync(Register registerModel);
    }
}