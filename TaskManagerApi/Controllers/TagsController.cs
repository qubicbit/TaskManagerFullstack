using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs.Tags;
using TaskManagerApi.Services.Tags;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // Alla får läsa tags
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _tagService.GetAllAsync());

    // Endast admin får skapa
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(TagCreateDto dto)
        => Ok(await _tagService.CreateAsync(dto));

    // Endast admin får uppdatera
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TagUpdateDto dto)
    {
        var success = await _tagService.UpdateAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    // Endast admin får ta bort
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _tagService.DeleteAsync(id);
        return success ? Ok() : NotFound();
    }
}
