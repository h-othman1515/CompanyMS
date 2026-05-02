using System.ComponentModel.DataAnnotations;

namespace CompanyMS.Models;

public class Department
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Navigation
    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();
}