using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services.Admin;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")] // Endast admin får använda denna controller
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// Hämtar statistik för admin-dashboard.
    /// Detta är cross‑resource och hör inte hemma i en resurs-controller.
    /// </summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
        => Ok(await _adminService.GetDashboardAsync());


    [HttpGet("users/{userId}/tasks")]
    public async Task<IActionResult> GetTasksForUser(string userId)
    {
        var tasks = await _adminService.GetTasksForUserAsync(userId);
        return Ok(tasks);
    }


    [HttpGet("users/{userId}/comments")]
    public async Task<IActionResult> GetCommentsForUser(string userId)
    {
        var comments = await _adminService.GetCommentsForUserAsync(userId);
        return Ok(comments);
    }


}
