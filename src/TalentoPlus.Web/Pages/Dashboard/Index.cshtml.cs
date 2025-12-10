using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDashboardService _dashboardService; // Asumimos que ya lo tienes inyectado

        public IndexModel(IEmployeeService employeeService, IDashboardService dashboardService)
        {
            _employeeService = employeeService;
            _dashboardService = dashboardService;
        }

        public DashboardMetricsDto Metrics { get; set; } = new();
        [BindProperty]
        public string? AISuggestion { get; set; }
        public string? AIResponse { get; set; }

        public async Task OnGetAsync()
        {
            // Carga los KPIs usando los servicios que ya funcionan
            Metrics = new DashboardMetricsDto
            {
                TotalEmployees = await _employeeService.GetTotalEmployeesCountAsync(),
                ActiveEmployees = await _employeeService.GetEmployeesByStatusNameCountAsync("Activo"),
                VacationEmployees = await _employeeService.GetEmployeesByStatusNameCountAsync("Vacaciones")
            };
        }

        public async Task<IActionResult> OnPostAskAIAsync()
        {
            if (string.IsNullOrWhiteSpace(AISuggestion))
            {
                ModelState.AddModelError("", "Debes escribir una pregunta.");
                await OnGetAsync(); // Recarga métricas
                return Page();
            }
            
            await OnGetAsync(); // Recarga métricas antes de la IA
            
            // Aquí llama al servicio de IA que implementaremos en el siguiente paso
            AIResponse = await _dashboardService.AskAIAsync(AISuggestion);

            return Page();
        }
    }
}