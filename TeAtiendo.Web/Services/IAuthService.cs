using TeAtiendo.Web.Models.Requests;
using TeAtiendo.Web.Models.Reponses;

namespace TeAtiendo.Web.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task Logout();
        Task LoadSession();
        bool IsAuthenticated { get; }
        UserInfo? CurrentUser { get; }
        string? Token { get; }
    }
}