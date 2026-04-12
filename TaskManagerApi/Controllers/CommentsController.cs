using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerApi.DTOs.Comments;
using TaskManagerApi.Services.Comments;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/tasks/{taskId:int}/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    private string? GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    // ---------------------------------------------------------
    // PUBLIC + USER: GET COMMENTS
    // ---------------------------------------------------------
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetComments(int taskId)
    {
        var userId = GetUserId();
        var comments = await _commentService.GetPublicAsync(taskId, userId);
        return Ok(comments);
    }

    // ---------------------------------------------------------
    // USER: GET SINGLE COMMENT (NEW)
    // ---------------------------------------------------------
    [Authorize]
    [HttpGet("me/{id:int}")]
    public async Task<IActionResult> GetMyCommentById(int taskId, int id)
    {
        var userId = GetUserId()!;
        var comment = await _commentService.GetByIdForUserAsync(taskId, id, userId);
        return comment == null ? NotFound() : Ok(comment);
    }

    // ---------------------------------------------------------
    // USER: CREATE COMMENT
    // ---------------------------------------------------------
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(int taskId, CommentCreateDto dto)
    {
        var userId = GetUserId()!;
        var created = await _commentService.CreateAsync(taskId, userId, dto);
        return created == null ? NotFound() : Ok(created);
    }

    // ---------------------------------------------------------
    // USER: UPDATE COMMENT
    // ---------------------------------------------------------
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int taskId, int id, CommentUpdateDto dto)
    {
        var userId = GetUserId()!;
        var success = await _commentService.UpdateAsync(taskId, id, userId, dto);
        return success ? Ok() : NotFound();
    }

    // ---------------------------------------------------------
    // USER: DELETE COMMENT
    // ---------------------------------------------------------
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int taskId, int id)
    {
        var userId = GetUserId()!;
        var success = await _commentService.DeleteAsync(taskId, id, userId);
        return success ? Ok() : NotFound();
    }

    // ---------------------------------------------------------
    // ADMIN: GET ALL COMMENTS FOR TASK
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpGet("admin/all")]
    public async Task<IActionResult> GetAllForAdmin(int taskId)
        => Ok(await _commentService.GetAllForTaskAsAdminAsync(taskId));

    // ---------------------------------------------------------
    // ADMIN: DELETE ANY COMMENT
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpDelete("admin/{id:int}")]
    public async Task<IActionResult> DeleteAsAdmin(int id)
    {
        var success = await _commentService.DeleteAsAdminAsync(id);
        return success ? Ok() : NotFound();
    }
}
