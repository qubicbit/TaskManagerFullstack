using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagerApi.DTOs.Users;
using TaskManagerApi.DTOs.Users.Admin;
using TaskManagerApi.Services.Users;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // ---------------------------------------------------------
    // USER: GET ME
    // ---------------------------------------------------------
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetMeAsync(userId);
        return Ok(user);
    }

    // ---------------------------------------------------------
    // USER: UPDATE ME
    // ---------------------------------------------------------
    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(UserUpdateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _userService.UpdateMeAsync(userId, dto);
        return success ? Ok() : BadRequest();
    }

    // ---------------------------------------------------------
    // ADMIN: GET ALL USERS
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    // ---------------------------------------------------------
    // ADMIN: GET USER BY ID
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    // ---------------------------------------------------------
    // ADMIN: UPDATE USER
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UserAdminUpdateDto dto)
    {
        var success = await _userService.UpdateUserAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    // ---------------------------------------------------------
    // ADMIN: DELETE USER
    // ---------------------------------------------------------
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var success = await _userService.DeleteUserAsync(id);
        return success ? Ok() : NotFound();
    }
}
