using TalentoPlus.Application.DTOs;

namespace TalentoPlus.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardMetricsDto> GetMetricsAsync();
        Task<string> AskAIAsync(string question); // Para la IA
    }
}