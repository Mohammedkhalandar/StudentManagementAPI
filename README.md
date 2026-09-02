# 🎓 Student Management API

A professional **ASP.NET Core Web API** for managing students with authentication, authorization, SQL Server integration, API versioning, logging, health checks, and Swagger documentation.

---

## 🚀 Features

- 👨‍🎓 Student CRUD Operations
- 🔐 JWT Authentication
- 📝 User Registration
- 🔑 User Login
- 🔒 Password Hashing
- 👤 Role-Based Authorization
- 🗄️ SQL Server Database
- ⚡ Entity Framework Core
- 🔄 API Versioning
- 📚 Swagger / OpenAPI Documentation
- 🩺 Health Check Endpoint
- 📋 Global Exception Handling Middleware
- 📊 Serilog Logging
- 🧱 DTO Architecture
- 🔐 Unique Email Validation

---

## 🛠️ Technologies Used

| Technology | Purpose |
|---|---|
| C# | Programming Language |
| .NET 10 | Backend Framework |
| ASP.NET Core Web API | API Development |
| Entity Framework Core | ORM |
| SQL Server | Database |
| JWT | Authentication |
| ASP.NET Identity PasswordHasher | Password Security |
| Swagger | API Documentation |
| Serilog | Application Logging |
| API Versioning | API Version Management |

---

# 📁 Project Structure

```text
StudentManagementAPI
│
├── Controllers
│   ├── AuthController.cs
│   └── StudentsController.cs
│
├── DTOs
│   ├── CreateStudentDto.cs
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── StudentDto.cs
│   └── UpdateStudentDto.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Middleware
│   └── ExceptionMiddleware.cs
│
├── Migrations
│
├── Models
│   ├── Student.cs
│   └── User.cs
│
├── Services
│   ├── IAuthService.cs
│   ├── AuthService.cs
│   ├── IStudentService.cs
│   └── StudentService.cs
│
├── Program.cs
│
└── StudentManagementAPI.csproj
