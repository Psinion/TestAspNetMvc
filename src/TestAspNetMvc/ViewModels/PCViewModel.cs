using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class PCViewModel : IBaseViewModel
{
    public string Title => "Компьютеры";
    public IEnumerable<PC> PCs { get; set; }
}