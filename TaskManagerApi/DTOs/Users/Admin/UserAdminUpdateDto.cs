using Microsoft.AspNetCore.Identity;
using System.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs.Users.Admin
{
    //UserUpdateDto ska INTE mappas direkt till ApplicationUser via AutoMapper.

    //Varför?
    //För att:
    //UserUpdateDto innehåller endast ändringsbara fält
    //Admin får ändra FullName, IsActive, Role
    //Men Role hanteras via UserManager.AddToRoleAsync / RemoveFromRoleAsync
    //Och IsActive är ett eget fält i ApplicationUser

    public class UserAdminUpdateDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
    }
}
