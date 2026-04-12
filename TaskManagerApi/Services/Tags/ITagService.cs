using TaskManagerApi.DTOs.Tags;

namespace TaskManagerApi.Services.Tags;

public interface ITagService
{
    Task<List<TagReadDto>> GetAllAsync();
    Task<TagReadDto> CreateAsync(TagCreateDto dto);
    Task<bool> UpdateAsync(int id, TagUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
