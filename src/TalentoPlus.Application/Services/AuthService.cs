using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork unitOfWork, IEmailService emailService, 
                          IConfiguration configuration, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            _logger.LogInformation($"Login intent: {request.DocumentNumber}");
            
            // Validación simple
            var employee = await _unitOfWork.Employees
                .FindAsync(e => e.DocumentNumber == request.DocumentNumber && 
                               e.Email == request.Email);
            
            var emp = employee.FirstOrDefault();
            if (emp == null)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            // Token SIMPLIFICADO para demo
            var token = $"jwt-{emp.Id}-{Guid.NewGuid()}";
            
            return new AuthResponseDto
            {
                Token = token,
                Message = "Login exitoso",
                Employee = new
                {
                    emp.Id,
                    emp.FirstName,
                    emp.LastName,
                    emp.Email
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            _logger.LogInformation($"Registro: {request.FirstName} {request.LastName}");
            
            // Crear empleado
            var employee = new Domain.Entities.Employee
            {
                DocumentNumber = request.DocumentNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                DepartmentId = request.DepartmentId,
                HireDate = DateOnly.FromDateTime(DateTime.Today),
                IsActive = true,
                // Valores por defecto
                JobTitleId = 1,
                EducationLevelId = 1,
                EmploymentStatusId = 1
            };

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            // ✅ EMAIL REAL (log)
            await _emailService.SendWelcomeEmailAsync(request.Email, $"{request.FirstName} {request.LastName}");

            var token = $"jwt-{employee.Id}-{Guid.NewGuid()}";

            return new AuthResponseDto
            {
                Token = token,
                Message = "Registro exitoso. Email de bienvenida enviado.",
                Employee = new
                {
                    employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email
                }
            };
        }
    }
}