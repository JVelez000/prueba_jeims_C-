-- 1. Tabla de Departamentos
CREATE TABLE IF NOT EXISTS Departments (
                                           Id SERIAL PRIMARY KEY,
                                           Name VARCHAR(100) NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 2. Tabla de Niveles Educativos
CREATE TABLE IF NOT EXISTS EducationLevels (
                                               Id SERIAL PRIMARY KEY,
                                               LevelName VARCHAR(50) NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 3. Tabla de Estados Laborales
CREATE TABLE IF NOT EXISTS EmploymentStatuses (
                                                  Id SERIAL PRIMARY KEY,
                                                  StatusName VARCHAR(50) NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 4. Tabla de Cargos
CREATE TABLE IF NOT EXISTS JobTitles (
                                         Id SERIAL PRIMARY KEY,
                                         TitleName VARCHAR(100) NOT NULL,
    DepartmentId INT REFERENCES Departments(Id),
    Description TEXT,
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 5. Tabla PRINCIPAL de Empleados
CREATE TABLE IF NOT EXISTS Employees (
                                         Id SERIAL PRIMARY KEY,
                                         DocumentNumber VARCHAR(20) UNIQUE NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    BirthDate DATE,
    HireDate DATE NOT NULL,
    Salary DECIMAL(10,2) CHECK (Salary >= 0),

    -- Relaciones
    DepartmentId INT REFERENCES Departments(Id),
    JobTitleId INT REFERENCES JobTitles(Id),
    EducationLevelId INT REFERENCES EducationLevels(Id),
    EmploymentStatusId INT REFERENCES EmploymentStatuses(Id),

    -- Información adicional
    ProfessionalProfile TEXT,
    Address TEXT,
    EmergencyContactName VARCHAR(200),
    EmergencyContactPhone VARCHAR(20),

    -- Control
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 6. Tabla para credenciales de empleados
CREATE TABLE IF NOT EXISTS EmployeeCredentials (
                                                   Id SERIAL PRIMARY KEY,
                                                   EmployeeId INT UNIQUE REFERENCES Employees(Id) ON DELETE CASCADE,
    PasswordHash VARCHAR(255) NOT NULL,
    PasswordSalt VARCHAR(255) NOT NULL,
    LastLogin TIMESTAMP NULL,
    LoginAttempts INT DEFAULT 0,
    IsLocked BOOLEAN DEFAULT FALSE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 7. Tabla para registros de auditoría
CREATE TABLE IF NOT EXISTS AuditLogs (
                                         Id SERIAL PRIMARY KEY,
                                         TableName VARCHAR(50) NOT NULL,
    RecordId INT NOT NULL,
    Action VARCHAR(20) NOT NULL,
    OldValues JSONB,
    NewValues JSONB,
    UserId VARCHAR(100),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

-- 8. Tabla para logs de importación Excel
CREATE TABLE IF NOT EXISTS ImportLogs (
                                          Id SERIAL PRIMARY KEY,
                                          FileName VARCHAR(255) NOT NULL,
    TotalRecords INT NOT NULL,
    SuccessRecords INT NOT NULL,
    FailedRecords INT NOT NULL,
    ImportedBy VARCHAR(100),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );