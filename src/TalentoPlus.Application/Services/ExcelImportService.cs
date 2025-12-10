using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;

namespace TalentoPlus.Application.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcelImportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ImportEmployeesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Archivo vacío.");

            int processedCount = 0;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    try
                    {
                        var documento = GetValue(row, 0);
                        if (string.IsNullOrEmpty(documento)) continue;

                        var nombres = GetValue(row, 1);
                        var apellidos = GetValue(row, 2);
                        var fechaNacText = GetValue(row, 3);
                        var direccion = GetValue(row, 4);
                        var telefono = GetValue(row, 5);
                        var email = GetValue(row, 6);
                        
                        var cargoNombre = GetValue(row, 7);
                        var salarioText = GetValue(row, 8);
                        var fechaIngresoText = GetValue(row, 9);
                        var estadoNombre = GetValue(row, 10);
                        var nivelEduNombre = GetValue(row, 11);
                        var perfil = GetValue(row, 12);
                        var deptoNombre = GetValue(row, 13);

                        int deptId = await GetOrCreateDepartmentAsync(deptoNombre);
                        int jobId = await GetOrCreateJobTitleAsync(cargoNombre, deptId);
                        int eduId = await GetOrCreateEducationLevelAsync(nivelEduNombre);
                        int statusId = await GetOrCreateStatusAsync(estadoNombre);

                        DateOnly.TryParse(fechaNacText, out DateOnly birthDate);
                        DateOnly.TryParse(fechaIngresoText, out DateOnly hireDate);
                        decimal.TryParse(salarioText, out decimal salary);

                        // Buscar si existe
                        var existingList = await _unitOfWork.Employees.FindAsync(e => e.DocumentNumber == documento);
                        var employee = existingList.FirstOrDefault();

                        if (employee == null)
                        {
                            employee = new Employee
                            {
                                DocumentNumber = documento,
                                FirstName = nombres,
                                LastName = apellidos,
                                Email = email,
                                Phone = telefono,
                                Address = direccion,
                                BirthDate = birthDate,
                                HireDate = hireDate == default ? DateOnly.FromDateTime(DateTime.Now) : hireDate,
                                Salary = salary,
                                ProfessionalProfile = perfil,
                                IsActive = true,
                                DepartmentId = deptId,
                                JobTitleId = jobId,
                                EducationLevelId = eduId,
                                EmploymentStatusId = statusId
                            };
                            await _unitOfWork.Employees.AddAsync(employee);
                        }
                        else
                        {
                            employee.FirstName = nombres;
                            employee.LastName = apellidos;
                            employee.Email = email;
                            employee.Phone = telefono;
                            employee.Address = direccion;
                            employee.Salary = salary;
                            employee.DepartmentId = deptId;
                            employee.JobTitleId = jobId;
                            employee.EducationLevelId = eduId;
                            employee.EmploymentStatusId = statusId;
                            employee.ProfessionalProfile = perfil;
                            
                            await _unitOfWork.Employees.UpdateAsync(employee);
                        }

                        processedCount++;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                
                await _unitOfWork.CompleteAsync();
            }

            return processedCount;
        }

        private string GetValue(IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null) return string.Empty;
            
            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
            {
                // CORRECCIÓN AQUÍ: Usamos 'GetValueOrDefault()' o '??' para evitar error de nullable
                DateTime dateValue = cell.DateCellValue ?? DateTime.MinValue;
                return dateValue.ToString("yyyy-MM-dd");
            }
            
            return cell.ToString()?.Trim() ?? string.Empty;
        }

        private async Task<int> GetOrCreateDepartmentAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = "Sin Departamento";
            var list = await _unitOfWork.Departments.FindAsync(d => d.Name.ToLower() == name.ToLower());
            var item = list.FirstOrDefault();
            if (item != null) return item.Id;

            var newItem = new Department { Name = name, Description = "Importado" };
            await _unitOfWork.Departments.AddAsync(newItem);
            await _unitOfWork.CompleteAsync();
            return newItem.Id;
        }

        private async Task<int> GetOrCreateJobTitleAsync(string name, int deptId)
        {
            if (string.IsNullOrWhiteSpace(name)) name = "Sin Cargo";
            var list = await _unitOfWork.JobTitles.FindAsync(j => j.TitleName.ToLower() == name.ToLower());
            var item = list.FirstOrDefault();
            if (item != null) return item.Id;

            var newItem = new JobTitle { TitleName = name, DepartmentId = deptId, IsActive = true };
            await _unitOfWork.JobTitles.AddAsync(newItem);
            await _unitOfWork.CompleteAsync();
            return newItem.Id;
        }

        private async Task<int> GetOrCreateEducationLevelAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = "No especificado";
            var list = await _unitOfWork.EducationLevels.FindAsync(e => e.LevelName.ToLower() == name.ToLower());
            var item = list.FirstOrDefault();
            if (item != null) return item.Id;

            var newItem = new EducationLevel { LevelName = name };
            await _unitOfWork.EducationLevels.AddAsync(newItem);
            await _unitOfWork.CompleteAsync();
            return newItem.Id;
        }

        private async Task<int> GetOrCreateStatusAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) name = "Activo";
            var list = await _unitOfWork.EmploymentStatuses.FindAsync(e => e.StatusName.ToLower() == name.ToLower());
            var item = list.FirstOrDefault();
            if (item != null) return item.Id;

            var newItem = new EmploymentStatus { StatusName = name };
            await _unitOfWork.EmploymentStatuses.AddAsync(newItem);
            await _unitOfWork.CompleteAsync();
            return newItem.Id;
        }
    }
}