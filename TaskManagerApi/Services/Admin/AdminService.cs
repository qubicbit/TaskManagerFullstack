using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Admin;
using TaskManagerApi.DTOs.Tasks;

namespace TaskManagerApi.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Samlar statistik från flera resurser (users, tasks, comments).
        /// Detta är cross‑resource och hör hemma i AdminService.
        /// </summary>
        public async Task<AdminDashboardDto> GetDashboardAsync()
        {
            var dto = new AdminDashboardDto
            {
                TotalUsers = await _context.Users.CountAsync(),
                ActiveUsers = await _context.Users.CountAsync(u => u.EmailConfirmed),

                TotalTasks = await _context.Tasks.CountAsync(),
                CompletedTasks = await _context.Tasks.CountAsync(t => t.IsCompleted),
                PublicTasks = await _context.Tasks.CountAsync(t => t.UserId == null),

                TotalComments = await _context.Comments.CountAsync()
            };

            return dto;
        }


        public async Task<List<TaskListItemDto>> GetTasksForUserAsync(string userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .Select(t => new TaskListItemDto ////Manuell mappning
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt,
                    CategoryName = t.Category.Name,
                    Tags = t.TaskTags.Select(tt => tt.Tag.Name).ToList()
                })
                .ToListAsync();
        }


        public async Task<List<AdminCommentDto>> GetCommentsForUserAsync(string userId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Task)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return comments.Select(c => new AdminCommentDto //Manuell mappning
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                TaskId = c.TaskId,
                TaskTitle = c.Task.Title,
                UserId = c.UserId,
                UserName = c.User.UserName ?? ""
            }).ToList();
        }



    }
}
