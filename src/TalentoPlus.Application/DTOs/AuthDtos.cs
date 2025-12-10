namespace TalentoPlus.Application.DTOs
{
    public class LoginRequestDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class RegisterRequestDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? Employee { get; set; }
    }
}