using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // ================================
    // GET ALL STUDENTS
    // Admin and User can access
    // ================================
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudents(
        string? search,
        string? sortBy,
        string? sortOrder,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _studentService.GetAllStudentsAsync(
            search,
            sortBy,
            sortOrder,
            page,
            pageSize);

        return Ok(result);
    }

    // ================================
    // GET STUDENT BY ID
    // Admin and User can access
    // ================================
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            return NotFound(new
            {
                message = $"Student with ID {id} was not found."
            });
        }

        return Ok(student);
    }

    // ================================
    // CREATE STUDENT
    // Admin only
    // ================================
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateStudent(
        CreateStudentDto createStudentDto)
    {
        var student = await _studentService
            .CreateStudentAsync(createStudentDto);

        return CreatedAtAction(
            nameof(GetStudentById),
            new
            {
                id = student.Id,
                version = "1"
            },
            student);
    }

    // ================================
    // UPDATE STUDENT
    // Admin only
    // ================================
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateStudent(
        int id,
        UpdateStudentDto updateStudentDto)
    {
        var student = await _studentService
            .UpdateStudentAsync(id, updateStudentDto);

        if (student == null)
        {
            return NotFound(new
            {
                message = $"Student with ID {id} was not found."
            });
        }

        return Ok(student);
    }

    // ================================
    // DELETE STUDENT
    // Admin only
    // ================================
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _studentService
            .DeleteStudentAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Student with ID {id} was not found."
            });
        }

        return NoContent();
    }
}