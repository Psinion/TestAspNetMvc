using System.ComponentModel.DataAnnotations;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class PCEditViewModel : IBaseViewModel
{
    public string Title => "Редактирование отдела";
    public int Id { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    public double Cpu { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    public double Memory { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    public double Hdd { get; set; }
}