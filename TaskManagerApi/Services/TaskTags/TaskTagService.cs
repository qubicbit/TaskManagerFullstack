using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Tags;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.TaskTags
{
    public class TaskTagService : ITaskTagService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskTagService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ---------------------------------------------------------
        // PUBLIC + USER: GET TAGS FOR PUBLIC OR OWN TASK
        // ---------------------------------------------------------
        public async Task<List<TagReadDto>> GetPublicAsync(int taskId, string? userId)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t =>
                    t.Id == taskId &&
                    (
                        t.UserId == null ||                     // public task
                        (userId != null && t.UserId == userId) // user's own task
                    )
                );

            if (task == null)
                return new();

            return _mapper.Map<List<TagReadDto>>(task.TaskTags.Select(tt => tt.Tag));
        }

        // ---------------------------------------------------------
        // USER: ADD TAGS TO OWN TASK
        // ---------------------------------------------------------
        public async Task<bool> AddTagsToTaskAsync(int taskId, string userId, List<int> tagIds)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return false;

            var newTags = tagIds
                .Where(tagId => !task.TaskTags.Any(tt => tt.TagId == tagId))
                .Select(tagId => new TaskTag
                {
                    TaskId = taskId,
                    TagId = tagId
                });

            _context.TaskTags.AddRange(newTags);
            await _context.SaveChangesAsync();

            return true;
        }

        // ---------------------------------------------------------
        // USER: REPLACE ALL TAGS FOR OWN TASK
        // ---------------------------------------------------------
        public async Task<bool> ReplaceTagsForTaskAsync(int taskId, string userId, List<int> tagIds)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return false;

            // Remove old tags
            _context.TaskTags.RemoveRange(task.TaskTags);

            // Add new tags
            var newTags = tagIds.Select(tagId => new TaskTag
            {
                TaskId = taskId,
                TagId = tagId
            });

            _context.TaskTags.AddRange(newTags);
            await _context.SaveChangesAsync();

            return true;
        }

        // ---------------------------------------------------------
        // USER: REMOVE TAG FROM OWN TASK
        // ---------------------------------------------------------
        public async Task<bool> RemoveTagFromTaskAsync(int taskId, string userId, int tagId)
        {
            var taskTag = await _context.TaskTags
                .Include(tt => tt.Task)
                .FirstOrDefaultAsync(tt =>
                    tt.TaskId == taskId &&
                    tt.TagId == tagId &&
                    tt.Task.UserId == userId
                );

            if (taskTag == null)
                return false;

            _context.TaskTags.Remove(taskTag);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
