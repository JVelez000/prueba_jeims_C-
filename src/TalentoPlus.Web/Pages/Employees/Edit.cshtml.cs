using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public EditModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public CreateEmployeeDto Employee { get; set; } = new();

        // Listas para los Selectores (Dropdowns)
        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> JobTitles { get; set; } = new();
        public List<SelectListItem> EducationLevels { get; set; } = new();
        public List<SelectListItem> EmploymentStatuses { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Obtener el empleado de la BD
            var employeeDto = await _employeeService.GetEmployeeByIdAsync(Id);
            if (employeeDto == null)
            {
                return NotFound();
            }

            // 2. Mapear los datos al modelo del formulario
            Employee = new CreateEmployeeDto
            {
                DocumentNumber = employeeDto.DocumentNumber,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                BirthDate = employeeDto.BirthDate,
                HireDate = employeeDto.HireDate,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId,
                JobTitleId = employeeDto.JobTitleId,
                EducationLevelId = employeeDto.EducationLevelId,
                EmploymentStatusId = employeeDto.EmploymentStatusId,
                ProfessionalProfile = employeeDto.ProfessionalProfile,
                Address = employeeDto.Address
            };

            // 3. Cargar las listas desde la BD
            await LoadDropdownData();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Si hay error de validación, recargamos las listas para que no salgan vacías
                await LoadDropdownData();
                return Page();
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(Id, Employee);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar empleado: {ex.Message}");
                await LoadDropdownData();
                return Page();
            }
        }

        private async Task LoadDropdownData()
        {
            // Cargar Departamentos
            var departments = await _employeeService.GetDepartmentsAsync();
            Departments = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();

            // Cargar Cargos
            var jobTitles = await _employeeService.GetJobTitlesAsync();
            JobTitles = jobTitles.Select(j => new SelectListItem
            {
                Value = j.Id.ToString(),
                Text = j.TitleName // Asegúrate que tu JobTitleDto tenga esta propiedad
            }).ToList();

            // Cargar Niveles Educativos
            var eduLevels = await _employeeService.GetEducationLevelsAsync();
            EducationLevels = eduLevels.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.LevelName // Asegúrate que tu EducationLevelDto tenga esta propiedad
            }).ToList();

            // Cargar Estados Laborales
            var statuses = await _employeeService.GetEmploymentStatusesAsync();
            EmploymentStatuses = statuses.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.StatusName // Asegúrate que tu EmploymentStatusDto tenga esta propiedad
            }).ToList();
        }
    }
}