using TaskManagerApi.DTOs.Auth;

namespace TaskManagerApi.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterDto dto);
        Task<(string? Token, string? Error, string? FullName, string? Email)> LoginAsync(LoginDto dto);
    }
}
