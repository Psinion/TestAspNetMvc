using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class ChangePCViewModel : IBaseViewModel
{
    public string Title => "Выдача компьютера пользователю";
    public int Id { get; set; }
    public string SearchString { get; set; }

    public string UserName { get; set; }

    public string DepartmentName { get; set; }

    public PC PC { get; set; }

    public IEnumerable<PC> FilteredPCs { get; set; }
}