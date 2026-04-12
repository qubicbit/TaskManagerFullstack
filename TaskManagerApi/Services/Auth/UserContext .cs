//using System.Security.Claims;

//namespace TaskManagerApi.Services.Auth
//{
//    // Läser JWT‑claims från HttpContext
//    public interface IUserContext
//    {
//        string? UserId { get; }
//        string? Email { get; }
//        string? Role { get; }
//    }

//    public class UserContext : IUserContext
//    {
//        private readonly IHttpContextAccessor _httpContext;

//        public UserContext(IHttpContextAccessor httpContext)
//        {
//            _httpContext = httpContext;
//        }

//        public string? UserId =>
//            _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

//        public string? Email =>
//            _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

//        public string? Role =>
//            _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
//    }
//}
