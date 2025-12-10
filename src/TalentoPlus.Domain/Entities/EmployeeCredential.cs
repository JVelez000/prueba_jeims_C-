namespace TalentoPlus.Domain.Entities
{
    public class EmployeeCredential : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public DateTime? LastLogin { get; set; }
        public int LoginAttempts { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
        
        // Navigation
        public Employee? Employee { get; set; }
    }
}
