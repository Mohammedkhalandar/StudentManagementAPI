using StudentManagementAPI.DTOs;

namespace StudentManagementAPI.Services;

public interface IStudentService
{
    Task<PagedResultDto<StudentDto>> GetAllStudentsAsync(
       string? search,
       string? sortBy,
       string? sortOrder,
       int page = 1,
       int pageSize = 10);
    Task<StudentDto?> GetStudentByIdAsync(int id);

    Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);

    Task<StudentDto?> UpdateStudentAsync(
        int id,
        UpdateStudentDto updateStudentDto);

    Task<bool> DeleteStudentAsync(int id);
}