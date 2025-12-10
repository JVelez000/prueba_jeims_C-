using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.Application.Services
{
    public class PdfService : IPdfService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;

        public PdfService(IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerateEmployeePdfAsync(int employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                throw new ArgumentException("Empleado no encontrado");

            return await GenerateEmployeePdfAsync(employee);
        }

        public async Task<byte[]> GenerateEmployeePdfAsync(EmployeeDto employee)
        {
            return await Task.Run(() =>
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(11));

                        page.Header()
                            .AlignCenter()
                            .Text("HOJA DE VIDA - TALENTOPLUS")
                            .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(column =>
                            {
                                // 1. INFORMACIÓN PERSONAL
                                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                                {
                                    col.Item().Text("INFORMACIÓN PERSONAL").Bold().FontSize(14);
                                    col.Spacing(5);
                                    AddRow(col, "Nombre completo:", $"{employee.FirstName} {employee.LastName}");
                                    AddRow(col, "Documento:", employee.DocumentNumber);
                                    AddRow(col, "Fecha nacimiento:", employee.BirthDate?.ToString("dd/MM/yyyy") ?? "N/A");
                                    AddRow(col, "Email:", employee.Email);
                                    AddRow(col, "Teléfono:", employee.Phone ?? "N/A");
                                });

                                column.Item().PaddingTop(15);

                                // 2. INFORMACIÓN LABORAL
                                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                                {
                                    col.Item().Text("INFORMACIÓN LABORAL").Bold().FontSize(14);
                                    col.Spacing(5);
                                    AddRow(col, "Cargo:", employee.JobTitleName);
                                    AddRow(col, "Departamento:", employee.DepartmentName);
                                    AddRow(col, "Estado:", employee.EmploymentStatusName);
                                    AddRow(col, "Fecha ingreso:", employee.HireDate.ToString("dd/MM/yyyy"));
                                    AddRow(col, "Salario:", $"${employee.Salary:N0}");
                                });

                                column.Item().PaddingTop(15);

                                // 3. INFORMACIÓN DE CONTACTO
                                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                                {
                                    col.Item().Text("INFORMACIÓN DE CONTACTO").Bold().FontSize(14);
                                    col.Spacing(5);
                                    AddRow(col, "Email:", employee.Email);
                                    AddRow(col, "Teléfono:", employee.Phone ?? "N/A");
                                    AddRow(col, "Dirección:", employee.Address ?? "N/A");
                                });

                                column.Item().PaddingTop(15);

                                // 4. INFORMACIÓN ACADÉMICA
                                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                                {
                                    col.Item().Text("INFORMACIÓN ACADÉMICA").Bold().FontSize(14);
                                    col.Spacing(5);
                                    AddRow(col, "Nivel educativo:", employee.EducationLevelName);
                                });

                                column.Item().PaddingTop(15);

                                // 5. PERFIL PROFESIONAL
                                if (!string.IsNullOrEmpty(employee.ProfessionalProfile))
                                {
                                    column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                                    {
                                        col.Item().Text("PERFIL PROFESIONAL").Bold().FontSize(14);
                                        col.Spacing(5);
                                        col.Item().Text(employee.ProfessionalProfile).FontSize(11);
                                    });
                                }
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Generado el: ").FontColor(Colors.Grey.Medium);
                                text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}").SemiBold();
                                text.Span(" - Página ").FontColor(Colors.Grey.Medium);
                                text.CurrentPageNumber();
                                text.Span(" de ").FontColor(Colors.Grey.Medium);
                                text.TotalPages();
                            });
                    });
                });

                return document.GeneratePdf();
            });
        }

        private void AddRow(ColumnDescriptor column, string label, string value)
        {
            column.Item().Row(row =>
            {
                row.RelativeItem(2).Text(label).SemiBold();
                row.RelativeItem(3).Text(value ?? "N/A");
            });
        }
    }
}