namespace TalentoPlus.Application.DTOs
{
    public class EmploymentStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}