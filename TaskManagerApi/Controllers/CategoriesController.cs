using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs.Categories;
using TaskManagerApi.Services.Categories;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Alla får läsa kategorier
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _categoryService.GetAllAsync());

    // Endast admin får skapa
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
        => Ok(await _categoryService.CreateAsync(dto));

    // Endast admin får uppdatera
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
    {
        var success = await _categoryService.UpdateAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    // Endast admin får ta bort
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _categoryService.DeleteAsync(id);
        return success ? Ok() : NotFound();
    }
}
