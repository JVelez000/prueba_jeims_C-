using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DashboardService(
            IEmployeeService employeeService, 
            IUnitOfWork unitOfWork, 
            IConfiguration configuration)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<DashboardMetricsDto> GetMetricsAsync()
        {
            return new DashboardMetricsDto
            {
                TotalEmployees = await _employeeService.GetTotalEmployeesCountAsync(),
                ActiveEmployees = await _employeeService.GetEmployeesByStatusNameCountAsync("Activo"),
                VacationEmployees = await _employeeService.GetEmployeesByStatusNameCountAsync("Vacaciones")
            };
        }

        public async Task<string> AskAIAsync(string question)
{
    Console.WriteLine($"\n🔍 [DashboardService] Pregunta: '{question}'");
    
    // DECLARAR variables AQUÍ para que estén disponibles en todo el método
    int total = 0, activos = 0, vacaciones = 0, inactivos = 0, licencia = 0, incapacidad = 0;
    string departmentData = string.Empty;
    
    try
    {
        // 1. Obtener TODOS los datos REALES de la base de datos
        total = await _employeeService.GetTotalEmployeesCountAsync();
        activos = await _employeeService.GetEmployeesByStatusNameCountAsync("Activo");
        vacaciones = await _employeeService.GetEmployeesByStatusNameCountAsync("Vacaciones");
        inactivos = await _employeeService.GetEmployeesByStatusNameCountAsync("Inactivo");
        licencia = await _employeeService.GetEmployeesByStatusNameCountAsync("Licencia");
        incapacidad = await _employeeService.GetEmployeesByStatusNameCountAsync("Incapacidad");

        // 2. Obtener datos por DEPARTAMENTO
        departmentData = await GetDepartmentStatistics();
        
        // 3. Obtener datos por CARGO
        var positionData = "• Los datos de cargos no están disponibles en este momento";
        
        // 4. Obtener datos por ESTADO
        var statusData = await GetStatusStatistics();

        // 5. Preparar contexto COMPLETO para la IA
        string context = BuildCompleteContext(total, activos, vacaciones, inactivos, licencia, incapacidad, 
                                            departmentData, positionData, statusData);

        // 6. Crear el prompt OPTIMIZADO
        string prompt = $@"INFORMACIÓN DEL SISTEMA DE RECURSOS HUMANOS:

{context}

INSTRUCCIONES PARA EL ASISTENTE:
Eres 'TalentoPlus Assistant', un asistente virtual especializado en Recursos Humanos.
Tu trabajo es ANALIZAR los datos proporcionados y responder preguntas del administrador.
Reglas estrictas:
1. Responde ÚNICAMENTE con la información de los datos anteriores
2. Sé CONCISO (máximo 3-4 frases) pero COMPLETO
3. Usa un tono AMABLE y PROFESIONAL
4. SIEMPRE menciona los números exactos cuando sea relevante
5. Si no tienes la información específica, dílo educadamente
6. Para preguntas comparativas (ej: '¿qué departamento tiene más?'), ANALIZA los datos y da una respuesta clara

PREGUNTA DEL ADMINISTRADOR: ""{question}""

RESPUESTA DEL ASISTENTE:";

        // 7. Obtener API Key de Gemini
        var apiKey = _configuration["Gemini:ApiKey"];
        Console.WriteLine($"🔍 [DashboardService] API Key: {(string.IsNullOrEmpty(apiKey) ? "NO CONFIGURADA" : "CONFIGURADA")}");

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("❌ [DashboardService] Usando FALLBACK (no hay API Key)");
            return CreateInformativeFallback(total, activos, vacaciones, inactivos, departmentData);
        }

        Console.WriteLine($"✅ [DashboardService] Llamando a Gemini API...");

        // 8. Llamar a la API de Gemini
        var requestUrl = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.0-flash:generateContent?key={apiKey}";
        
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.4,
                maxOutputTokens = 300,
                topP = 0.8,
                topK = 40
            }
        };

        var jsonContent = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(requestUrl, content);
        
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"✅ [DashboardService] Gemini API respondió exitosamente");
            var responseString = await response.Content.ReadAsStringAsync();
            
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(responseString))
                {
                    var candidates = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();
                    
                    if (!string.IsNullOrEmpty(candidates))
                    {
                        var cleanResponse = candidates.Trim();
                        
                        if (cleanResponse.Length < 20)
                        {
                            cleanResponse = $"¡Hola! {cleanResponse}";
                        }
                        
                        return cleanResponse;
                    }
                    
                    return "🤖 No recibí una respuesta clara. Por favor, reformula tu pregunta.";
                }
            }
            catch (JsonException)
            {
                return CreateInformativeFallback(total, activos, vacaciones, inactivos, departmentData);
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"❌ [DashboardService] Error Gemini API: {response.StatusCode}");
            Console.WriteLine($"❌ [DashboardService] Error detalle: {errorContent}");
            return CreateInformativeFallback(total, activos, vacaciones, inactivos, departmentData);
        }
    }
    catch (HttpRequestException httpEx)
    {
        Console.WriteLine($"❌ [DashboardService] Error HTTP: {httpEx.Message}");
        return CreateInformativeFallback(total, activos, vacaciones, inactivos, departmentData);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ [DashboardService] Excepción: {ex.Message}");
        Console.WriteLine($"❌ [DashboardService] Stack: {ex.StackTrace}");
        return CreateInformativeFallback(total, activos, vacaciones, inactivos, departmentData);
    }
}

        // ===== MÉTODOS AUXILIARES MEJORADOS =====

        private async Task<string> GetDepartmentStatistics()
        {
            try
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();
                var result = new StringBuilder();
                
                foreach (var dept in departments)
                {
                    var count = await _employeeService.GetEmployeesByDepartmentCountAsync(dept.Id);
                    if (count > 0)
                    {
                        result.AppendLine($"• {dept.Name}: {count} empleados");
                    }
                }
                
                return result.ToString();
            }
            catch
            {
                return "• No se pudieron cargar los datos de departamentos";
            }
        }

        private async Task<string> GetStatusStatistics()
        {
            try
            {
                var statuses = new[] { "Activo", "Inactivo", "Vacaciones", "Licencia", "Incapacidad" };
                var result = new StringBuilder();
                
                foreach (var status in statuses)
                {
                    var count = await _employeeService.GetEmployeesByStatusNameCountAsync(status);
                    result.AppendLine($"• {status}: {count} empleados");
                }
                
                return result.ToString();
            }
            catch
            {
                return "• No se pudieron cargar los datos por estado";
            }
        }

        private string BuildCompleteContext(int total, int activos, int vacaciones, int inactivos, 
            int licencia, int incapacidad, string departmentData, 
            string positionData, string statusData)
        {
            return $@"📊 RESUMEN GENERAL:
• Total de empleados: {total}
• Distribución por estados:
{statusData}

🏢 DISTRIBUCIÓN POR DEPARTAMENTOS:
{departmentData}

📈 PORCENTAJES:
• Activos: {CalculatePercentage(activos, total)}%
• Inactivos: {CalculatePercentage(inactivos, total)}%
• En vacaciones: {CalculatePercentage(vacaciones, total)}%";
        }

        private string CreateInformativeFallback(int total, int activos, int vacaciones, 
                                               int inactivos, string departmentData)
        {
            return $@"📊 INFORMACIÓN DEL SISTEMA:

RESUMEN GENERAL:
• Total empleados: {total}
• Activos: {activos} ({CalculatePercentage(activos, total)}%)
• Inactivos: {inactivos} ({CalculatePercentage(inactivos, total)}%)
• En vacaciones: {vacaciones} ({CalculatePercentage(vacaciones, total)}%)

DISTRIBUCIÓN POR DEPARTAMENTOS:
{departmentData}

(Nota: El asistente IA está en mantenimiento. Esta es información directa de la base de datos)";
        }

        private double CalculatePercentage(int value, int total)
        {
            if (total == 0) return 0;
            return Math.Round((value * 100.0) / total, 1);
        }

        private string ExtractDepartmentFromQuestion(string question)
        {
            var q = question.ToLower();
            
            if (q.Contains("tecnología") || q.Contains("tecnologia") || q.Contains("it") || q.Contains("sistemas")) 
                return "Tecnología";
            if (q.Contains("finanzas") || q.Contains("contabilidad") || q.Contains("financiero")) 
                return "Finanzas";
            if (q.Contains("recursos humanos") || q.Contains("rrhh") || q.Contains("personal")) 
                return "Recursos Humanos";
            if (q.Contains("ventas") || q.Contains("comercial")) 
                return "Ventas";
            if (q.Contains("marketing") || q.Contains("publicidad")) 
                return "Marketing";
            if (q.Contains("operaciones") || q.Contains("producción") || q.Contains("produccion")) 
                return "Operaciones";
            if (q.Contains("logística") || q.Contains("logistica")) 
                return "Logística";
            if (q.Contains("administración") || q.Contains("administracion") || q.Contains("admin")) 
                return "Administración";
            
            return string.Empty;
        }

        private async Task<int> GetDepartmentCount(string departmentName)
        {
            try
            {
                var deptList = await _unitOfWork.Departments
                    .FindAsync(d => d.Name.ToLower().Contains(departmentName.ToLower()));
                
                var dept = deptList.FirstOrDefault();
                return dept != null 
                    ? await _employeeService.GetEmployeesByDepartmentCountAsync(dept.Id)
                    : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}