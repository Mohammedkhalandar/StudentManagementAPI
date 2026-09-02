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

# 🛠️ Technologies Used

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

🔐 Authentication

The application uses JWT (JSON Web Token) authentication.

Register User
POST /api/v1/Auth/register

Example request:

{
  "name": "Mohammed",
  "email": "mohammed@example.com",
  "password": "YourPassword123"
}

Login
POST /api/v1/Auth/login

Example request:

{
  "email": "mohammed@example.com",
  "password": "YourPassword123"
}

Successful login returns a JWT token.

Example:

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

🔒 Authorization

Use the JWT token in Swagger.

Click:

Authorize 🔒

Enter:

Bearer YOUR_JWT_TOKEN

Then you can access protected API endpoints according to your configured authorization rules.

👨‍🎓 Student API Endpoints
Get All Students
GET /api/v1/Students
Get Student By ID
GET /api/v1/Students/{id}

Example:

GET /api/v1/Students/1
Create Student
POST /api/v1/Students

Example:

{
  "name": "Mohammed",
  "email": "mohammed@example.com",
  "age": 22,
  "course": "C# ASP.NET Core"
}
Update Student
PUT /api/v1/Students/{id}
Delete Student
DELETE /api/v1/Students/{id}
🩺 Health Check

The application provides a health check endpoint.

GET /health

Example response:

Healthy

The health check can verify the application and database health.

📚 Swagger Documentation

After running the application, open Swagger using the URL shown in the application console.

Example:

http://localhost:5245/swagger

Swagger allows you to:

Test API endpoints
Register users
Login
Get JWT tokens
Authorize using JWT
Test Student CRUD operations
⚙️ How to Run the Project
1️⃣ Clone the Repository
git clone https://github.com/Mohammedkhalandar/StudentManagementAPI.git

Navigate to the project:

cd StudentManagementAPI
2️⃣ Restore Packages
dotnet restore
3️⃣ Configure the Database

Create your local appsettings.json file.

Example:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  },

  "Jwt": {
    "Key": "YOUR_SECRET_KEY_HERE",
    "Issuer": "StudentManagementAPI",
    "Audience": "StudentManagementAPIUsers"
  }
}

⚠️ Never commit real JWT secret keys, passwords, or production connection strings to GitHub.

4️⃣ Apply Database Migrations
dotnet ef database update
5️⃣ Build the Project
dotnet build
6️⃣ Run the Application
dotnet run
📊 Logging

The project uses Serilog for logging.

Logs are written to:

Console
Daily rolling log files

Example:

Logs/log-YYYYMMDD.txt
🧱 Architecture

The project follows a layered architecture:

Controller
    ↓
Service
    ↓
Entity Framework Core
    ↓
SQL Server
Controllers

Handle HTTP requests and responses.

Services

Contain business logic.

DTOs

Transfer data between API clients and the application.

Models

Represent database entities.

Data Layer

Contains the Entity Framework Core DbContext.

Middleware

Handles global exceptions.

🔮 Future Improvements

Possible future improvements:

🧪 Unit Testing
🔄 Refresh Tokens
🐳 Docker Support
☁️ Cloud Deployment
⚙️ CI/CD with GitHub Actions
📊 Advanced Logging Dashboard
🔍 Search and Pagination
📧 Email Verification
🔐 Forgot Password Feature
👨‍💻 Author

Mohammed Khalandar

GitHub: https://github.com/Mohammedkhalandar

Repository: https://github.com/Mohammedkhalandar/StudentManagementAPI

⭐ If you found this project useful

Please consider giving the repository a Star ⭐!
