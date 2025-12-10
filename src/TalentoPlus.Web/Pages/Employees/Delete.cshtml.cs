using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public DeleteModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } // Enlace directo a la URL

        [BindProperty]
        public EmployeeDto Employee { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Usamos la propiedad Id directamente
            var employee = await _employeeService.GetEmployeeByIdAsync(Id);

            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // VALIDACIÓN CRÍTICA
            if (Id <= 0)
            {
                ModelState.AddModelError("", $"Error grave: El ID es {Id}. No se puede borrar.");
                return Page();
            }

            try 
            {
                Console.WriteLine($"🔥 BORRANDO EMPLEADO ID: {Id}");
                await _employeeService.DeleteEmployeeAsync(Id);
                Console.WriteLine("✅ BORRADO COMPLETADO");
                
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                ModelState.AddModelError("", "Error al eliminar: " + ex.Message);
                
                // Recargar datos para mostrar la página
                var emp = await _employeeService.GetEmployeeByIdAsync(Id);
                if(emp != null) Employee = emp;
                
                return Page();
            }
        }
    }
}