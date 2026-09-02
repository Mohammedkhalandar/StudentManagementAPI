using StudentManagementAPI.DTOs;

namespace StudentManagementAPI.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);

    Task<string?> LoginAsync(LoginDto loginDto);
}