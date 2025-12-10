using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentoPlus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MapExistingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCredentials_Employees_EmployeeId",
                table: "EmployeeCredentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EducationLevels_EducationLevelId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmploymentStatuses_EmploymentStatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTitles_Departments_DepartmentId",
                table: "JobTitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTitles",
                table: "JobTitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploymentStatuses",
                table: "EmploymentStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeCredentials",
                table: "EmployeeCredentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationLevels",
                table: "EducationLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "JobTitles",
                newName: "jobtitles");

            migrationBuilder.RenameTable(
                name: "EmploymentStatuses",
                newName: "employmentstatuses");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "EmployeeCredentials",
                newName: "employeecredentials");

            migrationBuilder.RenameTable(
                name: "EducationLevels",
                newName: "educationlevels");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "jobtitles",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "jobtitles",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TitleName",
                table: "jobtitles",
                newName: "title_name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "jobtitles",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "jobtitles",
                newName: "department_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "jobtitles",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_JobTitles_DepartmentId",
                table: "jobtitles",
                newName: "IX_jobtitles_department_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "employmentstatuses",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "employmentstatuses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "employmentstatuses",
                newName: "status_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "employmentstatuses",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "employees",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ProfessionalProfile",
                table: "employees",
                newName: "professional_profile");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "employees",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "JobTitleId",
                table: "employees",
                newName: "job_title_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "employees",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "HireDate",
                table: "employees",
                newName: "hire_date");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "employees",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "EmploymentStatusId",
                table: "employees",
                newName: "employment_status_id");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactPhone",
                table: "employees",
                newName: "emergency_contact_phone");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactName",
                table: "employees",
                newName: "emergency_contact_name");

            migrationBuilder.RenameColumn(
                name: "EducationLevelId",
                table: "employees",
                newName: "education_level_id");

            migrationBuilder.RenameColumn(
                name: "DocumentNumber",
                table: "employees",
                newName: "document_number");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "employees",
                newName: "department_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "employees",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "employees",
                newName: "birth_date");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Email",
                table: "employees",
                newName: "IX_employees_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_JobTitleId",
                table: "employees",
                newName: "IX_employees_job_title_id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmploymentStatusId",
                table: "employees",
                newName: "IX_employees_employment_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EducationLevelId",
                table: "employees",
                newName: "IX_employees_education_level_id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DocumentNumber",
                table: "employees",
                newName: "IX_employees_document_number");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentId",
                table: "employees",
                newName: "IX_employees_department_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "employeecredentials",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "employeecredentials",
                newName: "password_salt");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "employeecredentials",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LoginAttempts",
                table: "employeecredentials",
                newName: "login_attempts");

            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "employeecredentials",
                newName: "last_login");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "employeecredentials",
                newName: "is_locked");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "employeecredentials",
                newName: "employee_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "employeecredentials",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeCredentials_EmployeeId",
                table: "employeecredentials",
                newName: "IX_employeecredentials_employee_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "educationlevels",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "educationlevels",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "LevelName",
                table: "educationlevels",
                newName: "level_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "educationlevels",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "departments",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "departments",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "departments",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "departments",
                newName: "created_at");

            migrationBuilder.AddPrimaryKey(
                name: "PK_jobtitles",
                table: "jobtitles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employmentstatuses",
                table: "employmentstatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeecredentials",
                table: "employeecredentials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_educationlevels",
                table: "educationlevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_employee_credentials_employees",
                table: "employeecredentials",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments",
                table: "employees",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_education_levels",
                table: "employees",
                column: "education_level_id",
                principalTable: "educationlevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_employment_statuses",
                table: "employees",
                column: "employment_status_id",
                principalTable: "employmentstatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_job_titles",
                table: "employees",
                column: "job_title_id",
                principalTable: "jobtitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_job_titles_departments",
                table: "jobtitles",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employee_credentials_employees",
                table: "employeecredentials");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_education_levels",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_employment_statuses",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_job_titles",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_job_titles_departments",
                table: "jobtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jobtitles",
                table: "jobtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employmentstatuses",
                table: "employmentstatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeecredentials",
                table: "employeecredentials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_educationlevels",
                table: "educationlevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "jobtitles",
                newName: "JobTitles");

            migrationBuilder.RenameTable(
                name: "employmentstatuses",
                newName: "EmploymentStatuses");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "employeecredentials",
                newName: "EmployeeCredentials");

            migrationBuilder.RenameTable(
                name: "educationlevels",
                newName: "EducationLevels");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "JobTitles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "JobTitles",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "title_name",
                table: "JobTitles",
                newName: "TitleName");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "JobTitles",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "JobTitles",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "JobTitles",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_jobtitles_department_id",
                table: "JobTitles",
                newName: "IX_JobTitles_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "EmploymentStatuses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "EmploymentStatuses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "status_name",
                table: "EmploymentStatuses",
                newName: "StatusName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EmploymentStatuses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Employees",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "professional_profile",
                table: "Employees",
                newName: "ProfessionalProfile");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "job_title_id",
                table: "Employees",
                newName: "JobTitleId");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Employees",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "hire_date",
                table: "Employees",
                newName: "HireDate");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "employment_status_id",
                table: "Employees",
                newName: "EmploymentStatusId");

            migrationBuilder.RenameColumn(
                name: "emergency_contact_phone",
                table: "Employees",
                newName: "EmergencyContactPhone");

            migrationBuilder.RenameColumn(
                name: "emergency_contact_name",
                table: "Employees",
                newName: "EmergencyContactName");

            migrationBuilder.RenameColumn(
                name: "education_level_id",
                table: "Employees",
                newName: "EducationLevelId");

            migrationBuilder.RenameColumn(
                name: "document_number",
                table: "Employees",
                newName: "DocumentNumber");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "Employees",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Employees",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Employees",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_employees_Email",
                table: "Employees",
                newName: "IX_Employees_Email");

            migrationBuilder.RenameIndex(
                name: "IX_employees_job_title_id",
                table: "Employees",
                newName: "IX_Employees_JobTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_employment_status_id",
                table: "Employees",
                newName: "IX_Employees_EmploymentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_education_level_id",
                table: "Employees",
                newName: "IX_Employees_EducationLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_document_number",
                table: "Employees",
                newName: "IX_Employees_DocumentNumber");

            migrationBuilder.RenameIndex(
                name: "IX_employees_department_id",
                table: "Employees",
                newName: "IX_Employees_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "EmployeeCredentials",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "password_salt",
                table: "EmployeeCredentials",
                newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "EmployeeCredentials",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "login_attempts",
                table: "EmployeeCredentials",
                newName: "LoginAttempts");

            migrationBuilder.RenameColumn(
                name: "last_login",
                table: "EmployeeCredentials",
                newName: "LastLogin");

            migrationBuilder.RenameColumn(
                name: "is_locked",
                table: "EmployeeCredentials",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "EmployeeCredentials",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EmployeeCredentials",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_employeecredentials_employee_id",
                table: "EmployeeCredentials",
                newName: "IX_EmployeeCredentials_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "EducationLevels",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "EducationLevels",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "level_name",
                table: "EducationLevels",
                newName: "LevelName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EducationLevels",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Departments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Departments",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Departments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Departments",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTitles",
                table: "JobTitles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploymentStatuses",
                table: "EmploymentStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeCredentials",
                table: "EmployeeCredentials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationLevels",
                table: "EducationLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCredentials_Employees_EmployeeId",
                table: "EmployeeCredentials",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EducationLevels_EducationLevelId",
                table: "Employees",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmploymentStatuses_EmploymentStatusId",
                table: "Employees",
                column: "EmploymentStatusId",
                principalTable: "EmploymentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTitles_Departments_DepartmentId",
                table: "JobTitles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
