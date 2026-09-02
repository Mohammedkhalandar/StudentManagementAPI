namespace StudentManagementAPI.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    // Default role for newly registered users
    public string Role { get; set; } = "User";
}