using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL + SEARCH + SORTING + PAGINATION
    public async Task<PagedResultDto<StudentDto>> GetAllStudentsAsync(
        string? search,
        string? sortBy,
        string? sortOrder,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Students.AsQueryable();

        // SEARCH
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(student =>
                student.Name.Contains(search) ||
                student.Email.Contains(search) ||
                student.Course.Contains(search));
        }

        // SORTING
        query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
        {
            ("name", "desc") =>
                query.OrderByDescending(student => student.Name),

            ("name", _) =>
                query.OrderBy(student => student.Name),

            ("email", "desc") =>
                query.OrderByDescending(student => student.Email),

            ("email", _) =>
                query.OrderBy(student => student.Email),

            ("age", "desc") =>
                query.OrderByDescending(student => student.Age),

            ("age", _) =>
                query.OrderBy(student => student.Age),

            ("course", "desc") =>
                query.OrderByDescending(student => student.Course),

            ("course", _) =>
                query.OrderBy(student => student.Course),

            // DEFAULT SORTING
            _ =>
                query.OrderBy(student => student.Id)
        };

        // COUNT BEFORE PAGINATION
        var totalRecords = await query.CountAsync();

        // PAGINATION + DTO CONVERSION
        var students = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course
            })
            .ToListAsync();

        // PAGINATED RESPONSE
        return new PagedResultDto<StudentDto>
        {
            TotalRecords = totalRecords,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(
                totalRecords / (double)pageSize),
            Data = students
        };
    }


    // GET STUDENT BY ID
    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return null;
        }

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Course = student.Course
        };
    }


    // CREATE STUDENT
    public async Task<StudentDto> CreateStudentAsync(
        CreateStudentDto createStudentDto)
    {
        // CHECK IF EMAIL ALREADY EXISTS
        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(student =>
                student.Email == createStudentDto.Email);

        if (existingStudent != null)
        {
            throw new InvalidOperationException(
                "A student with this email already exists.");
        }

        // CREATE NEW STUDENT
        var student = new Student
        {
            Name = createStudentDto.Name,
            Email = createStudentDto.Email,
            Age = createStudentDto.Age,
            Course = createStudentDto.Course
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Course = student.Course
        };
    }


    // UPDATE STUDENT
    public async Task<StudentDto?> UpdateStudentAsync(
        int id,
        UpdateStudentDto updateStudentDto)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return null;
        }

        // CHECK IF ANOTHER STUDENT USES THIS EMAIL
        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s =>
                s.Email == updateStudentDto.Email &&
                s.Id != id);

        if (existingStudent != null)
        {
            throw new InvalidOperationException(
                "Another student with this email already exists.");
        }

        // UPDATE STUDENT
        student.Name = updateStudentDto.Name;
        student.Email = updateStudentDto.Email;
        student.Age = updateStudentDto.Age;
        student.Course = updateStudentDto.Course;

        await _context.SaveChangesAsync();

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Course = student.Course
        };
    }


    // DELETE STUDENT
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return false;
        }

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        return true;
    }
}   