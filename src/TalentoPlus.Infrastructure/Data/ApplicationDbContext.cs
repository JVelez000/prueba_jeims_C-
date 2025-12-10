using Microsoft.EntityFrameworkCore;
using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<EmploymentStatus> EmploymentStatuses { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<EmployeeCredential> EmployeeCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Mapear a tablas existentes en minúscula
            modelBuilder.Entity<Employee>().ToTable("employees");
            modelBuilder.Entity<Department>().ToTable("departments");
            modelBuilder.Entity<EducationLevel>().ToTable("educationlevels");
            modelBuilder.Entity<EmploymentStatus>().ToTable("employmentstatuses");
            modelBuilder.Entity<JobTitle>().ToTable("jobtitles");
            modelBuilder.Entity<EmployeeCredential>().ToTable("employeecredentials");

            // 2. Configuración de Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasIndex(e => e.DocumentNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Salary).HasPrecision(10, 2);
                
                // Mapear columnas (TODAS SIN underscore)
                entity.Property(e => e.DocumentNumber).HasColumnName("documentnumber");
                entity.Property(e => e.FirstName).HasColumnName("firstname");
                entity.Property(e => e.LastName).HasColumnName("lastname");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.BirthDate).HasColumnName("birthdate");
                entity.Property(e => e.HireDate).HasColumnName("hiredate");
                entity.Property(e => e.Salary).HasColumnName("salary");
                entity.Property(e => e.ProfessionalProfile).HasColumnName("professionalprofile");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.EmergencyContactName).HasColumnName("emergencycontactname");
                entity.Property(e => e.EmergencyContactPhone).HasColumnName("emergencycontactphone");
                entity.Property(e => e.IsActive).HasColumnName("isactive");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat");
                
                // Foreign keys (SIN underscore)
                entity.Property(e => e.DepartmentId).HasColumnName("departmentid");
                entity.Property(e => e.JobTitleId).HasColumnName("jobtitleid");
                entity.Property(e => e.EducationLevelId).HasColumnName("educationlevelid");
                entity.Property(e => e.EmploymentStatusId).HasColumnName("employmentstatusid");

                // Relaciones
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.JobTitle)
                    .WithMany(j => j.Employees)
                    .HasForeignKey(e => e.JobTitleId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.EducationLevel)
                    .WithMany(el => el.Employees)
                    .HasForeignKey(e => e.EducationLevelId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.EmploymentStatus)
                    .WithMany(es => es.Employees)
                    .HasForeignKey(e => e.EmploymentStatusId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 3. Configuración de Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(d => d.Name).HasColumnName("name");
                entity.Property(d => d.Description).HasColumnName("description");
                entity.Property(d => d.CreatedAt).HasColumnName("createdat");
                entity.Property(d => d.UpdatedAt).HasColumnName("updatedat");
            });

            // 4. Configuración de EducationLevel
            modelBuilder.Entity<EducationLevel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(el => el.LevelName).HasColumnName("levelname");
                entity.Property(el => el.Description).HasColumnName("description");
                entity.Property(el => el.CreatedAt).HasColumnName("createdat");
                entity.Property(el => el.UpdatedAt).HasColumnName("updatedat");

            });

            // 5. Configuración de EmploymentStatus
            modelBuilder.Entity<EmploymentStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(es => es.StatusName).HasColumnName("statusname");
                entity.Property(es => es.Description).HasColumnName("description");
                entity.Property(es => es.CreatedAt).HasColumnName("createdat");
                entity.Property(es => es.UpdatedAt).HasColumnName("updatedat");
            });

            // 6. Configuración de JobTitle
            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(j => j.TitleName).HasColumnName("titlename");
                entity.Property(j => j.Description).HasColumnName("description");
                entity.Property(j => j.IsActive).HasColumnName("isactive");
                entity.Property(j => j.DepartmentId).HasColumnName("departmentid");
                entity.Property(j => j.CreatedAt).HasColumnName("createdat");
                entity.Property(j => j.UpdatedAt).HasColumnName("updatedat");
                
                entity.HasOne(j => j.Department)
                    .WithMany(d => d.JobTitles)
                    .HasForeignKey(j => j.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 7. Configuración de EmployeeCredential
            modelBuilder.Entity<EmployeeCredential>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasIndex(ec => ec.EmployeeId).IsUnique();
                
                entity.Property(ec => ec.EmployeeId).HasColumnName("employeeid");
                entity.Property(ec => ec.PasswordHash).HasColumnName("passwordhash");
                entity.Property(ec => ec.PasswordSalt).HasColumnName("passwordsalt");
                entity.Property(ec => ec.LastLogin).HasColumnName("lastlogin");
                entity.Property(ec => ec.LoginAttempts).HasColumnName("loginattempts");
                entity.Property(ec => ec.IsLocked).HasColumnName("islocked");
                entity.Property(ec => ec.CreatedAt).HasColumnName("createdat");
                entity.Property(ec => ec.UpdatedAt).HasColumnName("updatedat");
                
                entity.HasOne(ec => ec.Employee)
                    .WithOne()
                    .HasForeignKey<EmployeeCredential>(ec => ec.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}