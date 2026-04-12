using TaskManagerApi.DTOs.Tasks;

public interface ITaskService
{
    // PUBLIC + USER
    Task<List<TaskListItemDto>> GetPublicAsync(string? userId);
    Task<TaskReadDto?> GetPublicByIdAsync(int id, string? userId);

    // USER
    Task<List<TaskListItemDto>> GetAllAsync(string userId);
    Task<TaskReadDto?> GetByIdForUserAsync(int id, string userId);
    Task<TaskReadDto> CreateAsync(string userId, TaskCreateDto dto);
    Task<bool> UpdateAsync(int id, string userId, TaskUpdateDto dto);
    Task<bool> DeleteAsync(int id, string userId);

    // ADMIN
    Task<List<TaskListItemDto>> GetAllForAdminAsync();
    Task<TaskReadDto?> GetByIdForAdminAsync(int id);
    Task<bool> DeleteAsAdminAsync(int id);
}
