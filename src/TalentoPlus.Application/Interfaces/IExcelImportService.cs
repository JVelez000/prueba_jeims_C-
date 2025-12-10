using Microsoft.AspNetCore.Http;

namespace TalentoPlus.Application.Interfaces
{
    public interface IExcelImportService
    {
        Task<int> ImportEmployeesAsync(IFormFile file);
    }
}