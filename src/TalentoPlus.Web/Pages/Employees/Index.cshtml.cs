using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPdfService _pdfService;

        public IndexModel(IEmployeeService employeeService, IPdfService pdfService)
        {
            _employeeService = employeeService;
            _pdfService = pdfService;
        }

        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        // Esta propiedad captura lo que escribas en el buscador
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            // Pasamos el término de búsqueda al servicio
            Employees = await _employeeService.GetAllEmployeesAsync(SearchString);
        }

        // NUEVO: Handler para descargar PDF
        public async Task<IActionResult> OnGetDownloadPdfAsync(int id)
        {
            try
            {
                var pdfBytes = await _pdfService.GenerateEmployeePdfAsync(id);
                
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                var fileName = $"Hoja_Vida_{employee?.FirstName}_{employee?.LastName}_{DateTime.Now:yyyyMMdd}.pdf";
                
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error generando PDF: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}