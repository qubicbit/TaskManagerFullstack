using TaskManagerApi.DTOs.Users;
using TaskManagerApi.DTOs.Users.Admin;

namespace TaskManagerApi.Services.Users
{
    public interface IUserService
    {
        // USER
        Task<UserReadDto?> GetMeAsync(string userId);
        Task<bool> UpdateMeAsync(string userId, UserUpdateDto dto);

        // ADMIN
        Task<List<UserListItemDto>> GetAllAsync();
        Task<UserAdminReadDto?> GetByIdAsync(string id);
        Task<bool> UpdateUserAsync(string id, UserAdminUpdateDto dto);
        Task<bool> DeleteUserAsync(string id);
    }
}
