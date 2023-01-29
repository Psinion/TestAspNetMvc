using System.ComponentModel.DataAnnotations;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class DepartmentEditViewModel : IBaseViewModel
{
    public string Title => "Редактирование отдела";
    public int Id { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    [DataType(DataType.Text)]
    public string Name { get; set; }
}