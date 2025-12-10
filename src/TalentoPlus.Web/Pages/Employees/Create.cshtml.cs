using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.Web.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateModel(
            IEmployeeService employeeService,
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public CreateEmployeeDto Employee { get; set; } = new CreateEmployeeDto();

        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> JobTitles { get; set; } = new();
        public List<SelectListItem> EducationLevels { get; set; } = new();
        public List<SelectListItem> EmploymentStatuses { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Valores por defecto
            Employee.HireDate = DateOnly.FromDateTime(DateTime.Today);
            Employee.Salary = 2000000;

            // Cargar datos para dropdowns (simplificado por ahora)
            await LoadDropdownData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return Page();
            }

            try
            {
                await _employeeService.CreateEmployeeAsync(Employee);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear empleado: {ex.Message}");
                await LoadDropdownData();
                return Page();
            }
        }

        private async Task LoadDropdownData()
        {
            // TODO: Implementar servicios reales para estos datos
            Departments = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Tecnología" },
                new SelectListItem { Value = "2", Text = "Recursos Humanos" },
                new SelectListItem { Value = "3", Text = "Finanzas" }
            };

            JobTitles = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Desarrollador Senior" },
                new SelectListItem { Value = "2", Text = "Desarrollador Junior" },
                new SelectListItem { Value = "3", Text = "Analista" }
            };

            EducationLevels = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Primaria" },
                new SelectListItem { Value = "2", Text = "Bachillerato" },
                new SelectListItem { Value = "3", Text = "Técnico" },
                new SelectListItem { Value = "4", Text = "Profesional" }
            };

            EmploymentStatuses = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Activo" },
                new SelectListItem { Value = "2", Text = "Vacaciones" },
                new SelectListItem { Value = "3", Text = "Inactivo" }
            };
        }
    }
}