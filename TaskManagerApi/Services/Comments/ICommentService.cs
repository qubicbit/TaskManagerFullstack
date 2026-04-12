using TaskManagerApi.DTOs.Comments;

namespace TaskManagerApi.Services.Comments;

public interface ICommentService
{
    // PUBLIC + USER
    Task<List<CommentReadDto>> GetPublicAsync(int taskId, string? userId);
    //Task<CommentReadDto?> GetPublicByIdAsync(int taskId, int id, string? userId);

    // USER
    Task<CommentReadDto?> GetByIdForUserAsync(int taskId, int id, string userId);
    Task<CommentReadDto?> CreateAsync(int taskId, string userId, CommentCreateDto dto);
    Task<bool> UpdateAsync(int taskId, int id, string userId, CommentUpdateDto dto);
    Task<bool> DeleteAsync(int taskId, int id, string userId);

    // ADMIN
    Task<List<CommentReadDto>> GetAllForTaskAsAdminAsync(int taskId);
    Task<bool> DeleteAsAdminAsync(int id);
}
