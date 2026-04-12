using TaskManagerApi.DTOs.Admin;
using TaskManagerApi.DTOs.Tasks;

namespace TaskManagerApi.Services.Admin
{
    public interface IAdminService
    {
        /// <summary>
        /// Hämtar statistik över hela systemet för admin-dashboard.
        /// </summary>
        Task<AdminDashboardDto> GetDashboardAsync();
        Task<List<TaskListItemDto>> GetTasksForUserAsync(string userId);
        Task<List<AdminCommentDto>> GetCommentsForUserAsync(string userId);


    }
}
