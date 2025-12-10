using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.API.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return Ok(departments.Select(d => new { d.Id, d.Name }));
        }
    }
}