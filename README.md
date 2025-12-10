TalentoPlus - Employee Management System
Features
✅ Completed
Employee CRUD - Full Create, Read, Update, Delete operations

Excel Import - Process employee data from Excel files

PDF Generation - Generate employee resumes in PDF format

AI Dashboard - Natural language queries about employee data

Docker Support - Complete docker-compose setup

Clean Architecture - Separation of concerns

Unit & Integration Tests


Quick Start
With Docker (Recommended)
bash
docker-compose up --build
Without Docker
Start PostgreSQL:

bash
docker run -d -p 5432:5432 \
  -e POSTGRES_DB=talentoplus_db \
  -e POSTGRES_USER=talentoplus_user \
  -e POSTGRES_PASSWORD=TempP@ss123! \
  postgres:16-alpine
Run Web Application (Admin):

bash
cd src/TalentoPlus.Web
dotnet run --urls "http://localhost:5000"
Run API (Employees):

bash
cd src/TalentoPlus.API
dotnet run --urls "http://localhost:5001"
Credentials
Web Admin
URL: http://localhost:5000

Email: admin@talentoplus.com

Password: Admin123!

API Endpoints
Public Endpoints (No authentication):
http
GET /api/departments
http
POST /api/auth/login
Content-Type: application/json
{
    "documentNumber": "123456789",
    "email": "employee@company.com"
}
http
POST /api/auth/register
Content-Type: application/json
{
    "documentNumber": "123456789",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@company.com",
    "phone": "3001234567",
    "departmentId": 1
}
Protected Endpoints (Require JWT):
http
GET /api/employees/me
Authorization: Bearer {your-jwt-token}
http
GET /api/employees/me/pdf
Authorization: Bearer {your-jwt-token}
Project Structure
text
src/
├── TalentoPlus.Domain/          # Domain layer (entities, interfaces)
├── TalentoPlus.Application/     # Application layer (services, DTOs)
├── TalentoPlus.Infrastructure/  # Infrastructure layer (data access)
├── TalentoPlus.Web/            # Web UI (Admin portal)
└── TalentoPlus.API/            # REST API (Employee portal)
Environment Variables
Web Application (TalentoPlus.Web/appsettings.json):
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=talentoplus_db;Username=talentoplus_user;Password=TempP@ss123!"
  }
}
API (TalentoPlus.API/appsettings.json):
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=talentoplus_db;Username=talentoplus_user;Password=TempP@ss123!"
  },
  "Jwt": {
    "Key": "supersecretkey12345supersecretkey12345",
    "ExpireMinutes": 60
  }
}
Testing
bash
# Run application tests
cd src/TalentoPlus.Application.Tests
dotnet test

# Run API tests
cd src/TalentoPlus.API.Tests
dotnet test
Docker Configuration
The solution includes:

PostgreSQL 16 database

Web application container

API container

Network configuration

Volume persistence

Technical Stack
Backend: ASP.NET Core 9.0, Entity Framework Core

Database: PostgreSQL

Authentication: JWT Tokens, ASP.NET Core Identity

PDF Generation: QuestPDF

Excel Processing: NPOI

Containerization: Docker, Docker Compose

Testing: xUnit, Moq

Notes
AI integration uses Google Gemini API (fallback to simulated responses)

Email service can be configured with SendGrid or SMTP

All sensitive data is managed via environment variables

The system follows Clean Architecture principles
