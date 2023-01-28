using TestAspNetMvc.Data.Models.Base;

namespace TestAspNetMvc.Data.Models;

public class User : Entity
{
    public string UserName { get; set; }
    public double Salary { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int PCId { get; set; }
    public PC PC { get; set; }
}