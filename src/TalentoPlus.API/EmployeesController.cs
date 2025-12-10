using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPdfService _pdfService;

        public EmployeesController(IEmployeeService employeeService, IPdfService pdfService)
        {
            _employeeService = employeeService;
            _pdfService = pdfService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            // Obtener ID del JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int employeeId))
                return Unauthorized();

            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("me/pdf")]
        public async Task<IActionResult> DownloadMyPdf()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int employeeId))
                return Unauthorized();

            var pdfBytes = await _pdfService.GenerateEmployeePdfAsync(employeeId);
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            
            var fileName = $"Hoja_Vida_{employee?.FirstName}_{employee?.LastName}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}