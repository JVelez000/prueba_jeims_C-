using AutoMapper;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region CRUD Empleados

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string? searchTerm = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Si no hay búsqueda, devolvemos todo normal
                var employees = await _unitOfWork.Employees.GetAllAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }

            // Convertimos a minúsculas para búsqueda insensible a mayúsculas
            var term = searchTerm.ToLower().Trim();

            // Usamos FindAsync del repositorio genérico
            var filteredEmployees = await _unitOfWork.Employees.FindAsync(e => 
                e.DocumentNumber.Contains(term) ||
                e.FirstName.ToLower().Contains(term) ||
                e.LastName.ToLower().Contains(term) ||
                e.Email.ToLower().Contains(term)
            );

            return _mapper.Map<IEnumerable<EmployeeDto>>(filteredEmployees);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<EmployeeDto>(employee);
        }
        
        public async Task<int> GetEmployeesByStatusNameCountAsync(string statusName)
        {
            return await _unitOfWork.Employees.GetCountByStatusNameAsync(statusName);
        }

        public async Task UpdateEmployeeAsync(int id, CreateEmployeeDto employeeDto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) throw new Exception($"Employee with ID {id} not found.");
            
            // Actualiza las propiedades de la entidad con los valores del DTO
            _mapper.Map(employeeDto, employee);
            
            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            // 1. Log para ver si entra al método
            Console.WriteLine($"Intentando borrar empleado ID: {id}");

            // 2. Llamada directa al repositorio SQL
            await _unitOfWork.Employees.DeleteByIdAsync(id);
            
            // 3. Log de éxito
            Console.WriteLine($"Comando de borrado enviado para ID: {id}");
        }

        #endregion

        #region Listas para Dropdowns (Selects)

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var list = await _unitOfWork.Departments.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(list);
        }

        public async Task<IEnumerable<JobTitleDto>> GetJobTitlesAsync()
        {
            var list = await _unitOfWork.JobTitles.GetAllAsync(); 
            return _mapper.Map<IEnumerable<JobTitleDto>>(list);
        }

        public async Task<IEnumerable<EducationLevelDto>> GetEducationLevelsAsync()
        {
            var list = await _unitOfWork.EducationLevels.GetAllAsync();
            return _mapper.Map<IEnumerable<EducationLevelDto>>(list);
        }

        public async Task<IEnumerable<EmploymentStatusDto>> GetEmploymentStatusesAsync()
        {
            var list = await _unitOfWork.EmploymentStatuses.GetAllAsync();
            return _mapper.Map<IEnumerable<EmploymentStatusDto>>(list);
        }

        #endregion

        #region Dashboard y Métricas

        public async Task<int> GetTotalEmployeesCountAsync()
        {
            return await _unitOfWork.Employees.GetTotalCountAsync();
        }

        public async Task<int> GetEmployeesByStatusCountAsync(int statusId)
        {
            return await _unitOfWork.Employees.GetCountByStatusAsync(statusId);
        }

        public async Task<int> GetEmployeesByDepartmentCountAsync(int departmentId)
        {
            return await _unitOfWork.Employees.GetCountByDepartmentAsync(departmentId);
        }

        #endregion
    }
}