using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace TalentoPlus.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDashboardService _dashboardService; 

        public IndexModel(IEmployeeService employeeService, IDashboardService dashboardService)
        {
            _employeeService = employeeService;
            _dashboardService = dashboardService;
        }

        // PROPIEDADES NECESARIAS PARA EL HTML
        public DashboardMetricsDto Metrics { get; set; } = new();
        
        [BindProperty]
        [Required(ErrorMessage = "Por favor, escribe una pregunta")]
        [MaxLength(200, ErrorMessage = "La pregunta no puede exceder 200 caracteres")]
        public string AISuggestion { get; set; } = string.Empty;
        
        public string AIResponse { get; set; } = string.Empty;
        public string LastQuestion { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Usar el servicio DashboardService para obtener métricas
            Metrics = await _dashboardService.GetMetricsAsync();
        }

        public async Task<IActionResult> OnPostAskAIAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar métricas si hay error de validación
                Metrics = await _dashboardService.GetMetricsAsync();
                return Page();
            }

            try
            {
                // Guardar la pregunta para mostrar en el chat
                LastQuestion = AISuggestion;
                
                // Llamar al servicio de IA
                AIResponse = await _dashboardService.AskAIAsync(AISuggestion);
                
                // Obtener métricas actualizadas
                Metrics = await _dashboardService.GetMetricsAsync();
                
                // Limpiar el campo de entrada (opcional, para que se borre después de enviar)
                AISuggestion = string.Empty;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                AIResponse = $"❌ Error: {ex.Message}. Por favor, intenta de nuevo.";
                Metrics = await _dashboardService.GetMetricsAsync();
            }

            return Page();
        }
    }
}