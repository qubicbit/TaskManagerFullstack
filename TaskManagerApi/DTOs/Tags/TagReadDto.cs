namespace TaskManagerApi.DTOs.Tags
{//resurs‑DTO. Se skillnad mellan TagDto i DTOs/Tasks/TagDto.cs 
    public class TagReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
