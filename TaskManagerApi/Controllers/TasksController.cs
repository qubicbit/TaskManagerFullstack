using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerApi.DTOs.Tasks;
using TaskManagerApi.Services.Tasks;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private string? GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        // ---------------------------------------------------------
        // PUBLIC + USER: GET PUBLIC TASKS + USER TASKS
        // ---------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicTasks()
        {
            var userId = GetUserId(); // null for public
            var tasks = await _taskService.GetPublicAsync(userId);
            return Ok(tasks);
        }

        // ---------------------------------------------------------
        // PUBLIC + USER: GET PUBLIC TASK OR USER TASK BY ID
        // ---------------------------------------------------------
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicTaskById(int id)
        {
            var userId = GetUserId(); // null for public
            var task = await _taskService.GetPublicByIdAsync(id, userId);
            return task == null ? NotFound() : Ok(task);
        }

        // ---------------------------------------------------------
        // USER: GET ONLY MY TASKS (NO PUBLIC TASKS)
        // ---------------------------------------------------------
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = GetUserId()!;
            var tasks = await _taskService.GetAllAsync(userId);
            return Ok(tasks);
        }

        // ---------------------------------------------------------
        // USER: GET ONE OF MY TASKS (NO PUBLIC TASKS)
        // ---------------------------------------------------------
        [Authorize]
        [HttpGet("me/{id:int}")]
        public async Task<IActionResult> GetMyTaskById(int id)
        {
            var userId = GetUserId()!;
            var task = await _taskService.GetByIdForUserAsync(id, userId);
            return task == null ? NotFound() : Ok(task);
        }

        // ---------------------------------------------------------
        // USER: CREATE TASK
        // ---------------------------------------------------------
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateDto dto)
        {
            var userId = GetUserId()!;
            var created = await _taskService.CreateAsync(userId, dto);

            return CreatedAtAction(nameof(GetPublicTaskById),
                new { id = created.Id }, created);
        }

        // ---------------------------------------------------------
        // USER: UPDATE TASK
        // ---------------------------------------------------------
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto dto)
        {
            var userId = GetUserId()!;
            var success = await _taskService.UpdateAsync(id, userId, dto);
            return success ? NoContent() : NotFound();
        }

        // ---------------------------------------------------------
        // USER: DELETE TASK
        // ---------------------------------------------------------
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = GetUserId()!;
            var success = await _taskService.DeleteAsync(id, userId);
            return success ? NoContent() : NotFound();
        }


        // ---------------------------------------------------------
        // ADMIN: GET ALL TASKS
        // ---------------------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllTasksForAdmin()
            => Ok(await _taskService.GetAllForAdminAsync());

        // ---------------------------------------------------------
        // ADMIN: GET ANY TASK
        // ---------------------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpGet("admin/{id:int}")]
        public async Task<IActionResult> GetTaskByIdForAdmin(int id)
        {
            var task = await _taskService.GetByIdForAdminAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        // ---------------------------------------------------------
        // ADMIN: DELETE ANY TASK
        // ---------------------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/{id:int}")]
        public async Task<IActionResult> DeleteTaskAsAdmin(int id)
        {
            var success = await _taskService.DeleteAsAdminAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
