using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManagerApi.DTOs.Users;
using TaskManagerApi.DTOs.Users.Admin;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // USER: GET ME
        public async Task<UserReadDto?> GetMeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserReadDto
            {
                Id = user.Id,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                Roles = roles.ToList()
            };
        }

        // USER: UPDATE ME
        public async Task<bool> UpdateMeAsync(string userId, UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            // FullName
            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;

            // Email (Identity requires SetEmailAsync)
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailResult = await _userManager.SetEmailAsync(user, dto.Email);
                if (!emailResult.Succeeded)
                    return false;

                // If email is username → update username too
                var usernameResult = await _userManager.SetUserNameAsync(user, dto.Email);
                if (!usernameResult.Succeeded)
                    return false;
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // ADMIN: GET ALL USERS
        public async Task<List<UserListItemDto>> GetAllAsync()
        {
            // 1. Hämta alla användare från databasen
            var users = await _userManager.Users.ToListAsync();

            // 2. Skapa listan som ska returneras
            var result = new List<UserListItemDto>();

            // 3. Hämta roller asynkront per användare
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);

                result.Add(new UserListItemDto
                {
                    Id = u.Id,
                    Email = u.Email ?? "",
                    UserName = u.UserName ?? "",
                    FullName = u.FullName,
                    IsActive = u.IsActive,
                    Role = roles.FirstOrDefault() // första rollen, om någon finns
                });
            }

            return result;
        }

        // ADMIN: GET USER BY ID
        public async Task<UserAdminReadDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserAdminReadDto
            {
                Id = user.Id,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                FullName = user.FullName,
                EmailConfirmed = user.EmailConfirmed,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = roles.ToList()
            };
        }

        // ADMIN: UPDATE USER
        public async Task<bool> UpdateUserAsync(string id, UserAdminUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;

            // FullName
            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;

            // Active flag
            if (dto.IsActive.HasValue)
                user.IsActive = dto.IsActive.Value;

            // Email (admin may update email too)
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailResult = await _userManager.SetEmailAsync(user, dto.Email);
                if (!emailResult.Succeeded)
                    return false;

                var usernameResult = await _userManager.SetUserNameAsync(user, dto.Email);
                if (!usernameResult.Succeeded)
                    return false;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return false;

            // Update role
            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return true;
        }

        // ADMIN: DELETE USER
        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
