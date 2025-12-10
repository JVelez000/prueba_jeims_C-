using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Import
{
    public class IndexModel : PageModel
    {
        private readonly IExcelImportService _excelImportService;

        public IndexModel(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [BindProperty]
        public IFormFile? UploadedFile { get; set; }

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null)
            {
                ErrorMessage = "Debes seleccionar un archivo.";
                return Page();
            }

            try
            {
                int count = await _excelImportService.ImportEmployeesAsync(UploadedFile);
                SuccessMessage = $"¡Proceso exitoso! Se procesaron {count} empleados.";
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error crítico: " + ex.Message;
            }

            return Page();
        }
    }
}