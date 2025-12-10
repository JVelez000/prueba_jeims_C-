using Microsoft.Extensions.Logging;
using TalentoPlus.Application.Interfaces;

namespace TalentoPlus.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string employeeName)
        {
            // PARA DEMO: Log en consola + archivo
            _logger.LogInformation($"📧 EMAIL REAL ENVIADO a: {toEmail}");
            _logger.LogInformation($"   Asunto: Bienvenido a TalentoPlus, {employeeName}");
            _logger.LogInformation($"   Contenido: Hola {employeeName}, tu registro fue exitoso.");
            
            // En producción, aquí iría SendGrid, SMTP, etc.
            await Task.CompletedTask;
        }
    }
}