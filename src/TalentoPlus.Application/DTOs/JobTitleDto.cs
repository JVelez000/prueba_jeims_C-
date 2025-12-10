namespace TalentoPlus.Application.DTOs
{
    public class JobTitleDto
    {
        public int Id { get; set; }
        public string TitleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}