using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs.Tags;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Tags;

namespace TaskManagerApi.Services.Tags;

public class TagService : ITagService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TagService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET ALL
    public async Task<List<TagReadDto>> GetAllAsync()
    {
        var tags = await _context.Tags.ToListAsync();
        return _mapper.Map<List<TagReadDto>>(tags);
    }

    // CREATE
    public async Task<TagReadDto> CreateAsync(TagCreateDto dto)
    {
        var tag = _mapper.Map<Tag>(dto);

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return _mapper.Map<TagReadDto>(tag);
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, TagUpdateDto dto)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            return false;

        tag.Name = dto.Name ?? tag.Name;

        await _context.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await _context.Tags
            .Include(t => t.TaskTags)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
            return false;

        _context.TaskTags.RemoveRange(tag.TaskTags);
        _context.Tags.Remove(tag);

        await _context.SaveChangesAsync();
        return true;
    }
}
