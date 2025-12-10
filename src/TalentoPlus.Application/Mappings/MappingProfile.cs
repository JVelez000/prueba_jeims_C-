using AutoMapper;
using TalentoPlus.Application.DTOs;
using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 1. Mapeos Simples (Tablas Catálogo)
            CreateMap<Department, DepartmentDto>();
            CreateMap<EducationLevel, EducationLevelDto>();
            CreateMap<EmploymentStatus, EmploymentStatusDto>();
            
            // Para JobTitle, mapeamos el nombre del departamento si está disponible
            CreateMap<JobTitle, JobTitleDto>()
                .ForMember(dest => dest.DepartmentName, 
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            // 2. Mapeos de Empleado (Lectura)
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, 
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.JobTitleName, 
                    opt => opt.MapFrom(src => src.JobTitle != null ? src.JobTitle.TitleName : string.Empty))
                .ForMember(dest => dest.EducationLevelName, 
                    opt => opt.MapFrom(src => src.EducationLevel != null ? src.EducationLevel.LevelName : string.Empty))
                .ForMember(dest => dest.EmploymentStatusName, 
                    opt => opt.MapFrom(src => src.EmploymentStatus != null ? src.EmploymentStatus.StatusName : string.Empty));

            // 3. Mapeos de Creación/Edición (Escritura)
            // ReverseMap permite ir de DTO -> Entidad (usado en Create y Update)
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
        }
    }
}