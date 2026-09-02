using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.DTOs;

public class UpdateStudentDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Course is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Course must be between 2 and 100 characters.")]
    public string Course { get; set; } = string.Empty;
}