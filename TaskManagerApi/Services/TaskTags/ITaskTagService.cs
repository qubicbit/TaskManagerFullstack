using TaskManagerApi.DTOs.Tags;

namespace TaskManagerApi.Services.TaskTags
{
    public interface ITaskTagService
    {
        // PUBLIC + USER: Läs tags för public tasks eller egna tasks
        Task<List<TagReadDto>> GetPublicAsync(int taskId, string? userId);

        // USER: Ändra tags på egen task
        Task<bool> AddTagsToTaskAsync(int taskId, string userId, List<int> tagIds);
        Task<bool> ReplaceTagsForTaskAsync(int taskId, string userId, List<int> tagIds);
        Task<bool> RemoveTagFromTaskAsync(int taskId, string userId, int tagId);
    }
}
