using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public DetailsModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public EmployeeDto Employee { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            return Page();
        }
    }
}