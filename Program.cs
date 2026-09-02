using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StudentManagementAPI.Data;
using StudentManagementAPI.Middleware;
using StudentManagementAPI.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ================================
// SERILOG LOGGING
// ================================

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ================================
// CONTROLLERS
// ================================

builder.Services.AddControllers();

// ================================
// API VERSIONING
// ================================

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);

        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ReportApiVersions = true;

        // Version comes from URL
        // Example: /api/v1/Students
        options.ApiVersionReader =
            new UrlSegmentApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";

        options.SubstituteApiVersionInUrl = true;
    });

// ================================
// SWAGGER
// ================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.ParameterLocation.Header,
            Description = "Enter your JWT token."
        });

    options.AddSecurityRequirement(document => new()
    {
        [
            new Microsoft.OpenApi.OpenApiSecuritySchemeReference(
                "Bearer",
                document)
        ] = []
    });
});

// ================================
// APPLICATION SERVICES
// ================================

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// ================================
// DATABASE
// ================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ================================
// HEALTH CHECKS
// ================================

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>(
        name: "database");

// ================================
// JWT AUTHENTICATION
// ================================

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        RoleClaimType = ClaimTypes.Role,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("JWT Authentication Failed:");
            Console.WriteLine(context.Exception.Message);

            return Task.CompletedTask;
        }
    };
});

// ================================
// BUILD APPLICATION
// ================================

var app = builder.Build();

// ================================
// SERILOG REQUEST LOGGING
// ================================

app.UseSerilogRequestLogging();

// ================================
// GLOBAL EXCEPTION MIDDLEWARE
// ================================

app.UseMiddleware<ExceptionMiddleware>();

// ================================
// SWAGGER
// ================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// ================================
// HTTPS
// ================================

app.UseHttpsRedirection();

// ================================
// AUTHENTICATION & AUTHORIZATION
// ================================

app.UseAuthentication();
app.UseAuthorization();

// ================================
// API CONTROLLERS
// ================================

app.MapControllers();

// ================================
// HEALTH CHECK ENDPOINT
// ================================

app.MapHealthChecks("/health");

// ================================
// RUN APPLICATION
// ================================

app.Run();