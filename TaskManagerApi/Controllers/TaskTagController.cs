using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerApi.Services.TaskTags;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/tasks/{taskId:int}/tags")]
public class TaskTagController : ControllerBase
{
    private readonly ITaskTagService _taskTagService;

    public TaskTagController(ITaskTagService taskTagService)
    {
        _taskTagService = taskTagService;
    }

    private string? GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    // ---------------------------------------------------------
    // PUBLIC + USER: GET TAGS FOR PUBLIC OR OWN TASK
    // ---------------------------------------------------------
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetTagsForTask(int taskId)
    {
        var userId = GetUserId(); // null for public
        var tags = await _taskTagService.GetPublicAsync(taskId, userId);
        return Ok(tags);
    }

    // ---------------------------------------------------------
    // USER: ADD TAGS TO OWN TASK
    // ---------------------------------------------------------
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddTags(int taskId, List<int> tagIds)
    {
        var userId = GetUserId()!;
        var success = await _taskTagService.AddTagsToTaskAsync(taskId, userId, tagIds);
        return success ? Ok() : NotFound();
    }

    // ---------------------------------------------------------
    // USER: REPLACE ALL TAGS FOR OWN TASK
    // ---------------------------------------------------------
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> ReplaceTags(int taskId, List<int> tagIds)
    {
        var userId = GetUserId()!;
        var success = await _taskTagService.ReplaceTagsForTaskAsync(taskId, userId, tagIds);
        return success ? Ok() : NotFound();
    }

    // ---------------------------------------------------------
    // USER: REMOVE TAG FROM OWN TASK
    // ---------------------------------------------------------
    [Authorize]
    [HttpDelete("{tagId:int}")]
    public async Task<IActionResult> RemoveTag(int taskId, int tagId)
    {
        var userId = GetUserId()!;
        var success = await _taskTagService.RemoveTagFromTaskAsync(taskId, userId, tagId);
        return success ? Ok() : NotFound();
    }
}
