using System.ComponentModel.DataAnnotations;

namespace CompanyMS.Models;

public class Employee
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Required, MaxLength(20)]
    public string NationalId { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Nationality { get; set; }

    [MaxLength(20)]
    public string? MaritalStatus { get; set; }

    public string? PhotoPath { get; set; }

    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string Password { get; set; } = string.Empty;

    // Navigation
    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();
}