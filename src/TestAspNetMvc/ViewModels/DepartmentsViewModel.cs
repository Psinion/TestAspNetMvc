using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class DepartmentsViewModel : IBaseViewModel
{
    public string Title => "Отделы";
    public IEnumerable<Department> Departments { get; set; }
}