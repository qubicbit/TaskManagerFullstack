using AutoMapper;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Comments;
using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Services.Comments;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CommentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ---------------------------------------------------------
    // PUBLIC + USER: GET COMMENTS FOR PUBLIC OR OWN TASK
    // ---------------------------------------------------------
    public async Task<List<CommentReadDto>> GetPublicAsync(int taskId, string? userId)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Task)
            .Where(c =>
                c.TaskId == taskId &&
                (
                    c.Task.UserId == null ||                     // public task
                    (userId != null && c.Task.UserId == userId) // user's own task
                )
            )
            .OrderByDescending(c => c.CreatedAt)   // nyast först
            .ToListAsync();

        var dtos = _mapper.Map<List<CommentReadDto>>(comments);

        foreach (var dto in dtos)
            dto.IsOwner = (dto.UserId == userId);

        return dtos;
    }


    // ---------------------------------------------------------
    // USER: GET SINGLE COMMENT BY ID
    // ---------------------------------------------------------
    public async Task<CommentReadDto?> GetByIdForUserAsync(int taskId, int id, string userId)
    {
        var comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.TaskId == taskId &&
                c.UserId == userId
            );

        if (comment == null)
            return null;

        var dto = _mapper.Map<CommentReadDto>(comment);
        dto.IsOwner = true; // eftersom userId matchade i queryn

        return dto;
    }

    // ---------------------------------------------------------
    // USER: CREATE COMMENT
    // ---------------------------------------------------------
    public async Task<CommentReadDto?> CreateAsync(int taskId, string userId, CommentCreateDto dto)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null)
            return null;

        var comment = new Comment
        {
            TaskId = taskId,
            UserId = userId,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        await _context.Entry(comment).Reference(c => c.User).LoadAsync();

        var result = _mapper.Map<CommentReadDto>(comment);
        result.IsOwner = true;

        return result;
    }

    // ---------------------------------------------------------
    // USER: UPDATE COMMENT
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(int taskId, int id, string userId, CommentUpdateDto dto)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.TaskId == taskId &&
                c.UserId == userId
            );

        if (comment == null)
            return false;

        if (dto.Content != null)
            comment.Content = dto.Content;

        comment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    // ---------------------------------------------------------
    // USER: DELETE COMMENT
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int taskId, int id, string userId)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.TaskId == taskId &&
                c.UserId == userId
            );

        if (comment == null)
            return false;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    // ---------------------------------------------------------
    // ADMIN: GET ALL COMMENTS FOR ANY TASK
    // ---------------------------------------------------------
    public async Task<List<CommentReadDto>> GetAllForTaskAsAdminAsync(int taskId)
    {
        var comments = await _context.Comments
            .Where(c => c.TaskId == taskId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)   // nyast först
            .ToListAsync();

        var dtos = _mapper.Map<List<CommentReadDto>>(comments);

        foreach (var dto in dtos)
            dto.IsOwner = false;

        return dtos;
    }


    // ---------------------------------------------------------
    // ADMIN: DELETE ANY COMMENT
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsAdminAsync(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            return false;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }
}
