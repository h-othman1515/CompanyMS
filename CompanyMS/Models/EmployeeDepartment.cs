using CompanyMS.Models;

namespace CompanyMS.Models;

public class EmployeeDepartment
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}