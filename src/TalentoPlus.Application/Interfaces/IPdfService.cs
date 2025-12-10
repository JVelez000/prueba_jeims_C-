using TalentoPlus.Application.DTOs;  // <-- using PRIMERO

namespace TalentoPlus.Application.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> GenerateEmployeePdfAsync(int employeeId);
        Task<byte[]> GenerateEmployeePdfAsync(EmployeeDto employee);
    }
}