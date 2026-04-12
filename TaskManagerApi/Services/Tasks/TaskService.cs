using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Tasks;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ---------------------------------------------------------
        // PUBLIC + USER: GET PUBLIC TASKS + USER TASKS
        // ---------------------------------------------------------
        public async Task<List<TaskListItemDto>> GetPublicAsync(string? userId)
        {
            var tasks = await _context.Tasks
                .Where(t =>
                    t.UserId == null ||
                    (userId != null && t.UserId == userId)
                )
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .OrderByDescending(t => t.CreatedAt)   // ny task först
                .ToListAsync();

            return _mapper.Map<List<TaskListItemDto>>(tasks);
        }

        // ---------------------------------------------------------
        // PUBLIC + USER: GET PUBLIC TASK OR USER TASK BY ID
        // ---------------------------------------------------------
        public async Task<TaskReadDto?> GetPublicByIdAsync(int id, string? userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .Include(t => t.Comments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(t =>
                    t.Id == id &&
                    (
                        t.UserId == null ||
                        (userId != null && t.UserId == userId)
                    )
                );

            return task == null ? null : _mapper.Map<TaskReadDto>(task);
        }

        // ---------------------------------------------------------
        // USER: GET ALL TASKS
        // ---------------------------------------------------------
        public async Task<List<TaskListItemDto>> GetAllAsync(string userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .OrderByDescending(t => t.CreatedAt)   // ny task först
                .ToListAsync();

            return _mapper.Map<List<TaskListItemDto>>(tasks);
        }

        // ---------------------------------------------------------
        // USER: GET SINGLE TASK BY ID
        // ---------------------------------------------------------
        public async Task<TaskReadDto?> GetByIdForUserAsync(int id, string userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .Include(t => t.Comments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return task == null ? null : _mapper.Map<TaskReadDto>(task);
        }

        // ---------------------------------------------------------
        // USER: CREATE TASK
        // ---------------------------------------------------------
        public async Task<TaskReadDto> CreateAsync(string userId, TaskCreateDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.UserId = userId;
            task.CreatedAt = DateTime.UtcNow;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            if (dto.TagIds.Any())
            {
                var tags = dto.TagIds.Select(id => new TaskTag
                {
                    TaskId = task.Id,
                    TagId = id
                });

                _context.TaskTags.AddRange(tags);
                await _context.SaveChangesAsync();
            }

            var created = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .FirstAsync(t => t.Id == task.Id);

            return _mapper.Map<TaskReadDto>(created);
        }

        // ---------------------------------------------------------
        // USER: UPDATE TASK
        // ---------------------------------------------------------
        public async Task<bool> UpdateAsync(int id, string userId, TaskUpdateDto dto)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return false;

            if (dto.Title != null) task.Title = dto.Title;
            if (dto.Description != null) task.Description = dto.Description;
            if (dto.IsCompleted.HasValue) task.IsCompleted = dto.IsCompleted.Value;
            if (dto.CategoryId.HasValue) task.CategoryId = dto.CategoryId;

            if (dto.TagIds != null)
            {
                _context.TaskTags.RemoveRange(task.TaskTags);

                var newTags = dto.TagIds.Select(id => new TaskTag
                {
                    TaskId = task.Id,
                    TagId = id
                });

                _context.TaskTags.AddRange(newTags);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------------------------------------------
        // USER: DELETE TASK
        // ---------------------------------------------------------
        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------------------------------------------
        // ADMIN: GET ALL TASKS
        // ---------------------------------------------------------
        public async Task<List<TaskListItemDto>> GetAllForAdminAsync()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .OrderByDescending(t => t.CreatedAt)   // ny task först
                .ToListAsync();

            return _mapper.Map<List<TaskListItemDto>>(tasks);
        }

        // ---------------------------------------------------------
        // ADMIN: GET ANY TASK
        // ---------------------------------------------------------
        public async Task<TaskReadDto?> GetByIdForAdminAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .Include(t => t.Comments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            return task == null ? null : _mapper.Map<TaskReadDto>(task);
        }

        // ---------------------------------------------------------
        // ADMIN: DELETE ANY TASK
        // ---------------------------------------------------------
        public async Task<bool> DeleteAsAdminAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
