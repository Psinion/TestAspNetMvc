using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels;

public class UserEditViewModel : IBaseViewModel
{
    public string Title => "Редактирование пользователя";
    public int Id { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    [DataType(DataType.Text)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    [DataType(DataType.Currency, ErrorMessage = ("Введите число"))]
    public double? Salary { get; set; }

    public int? DepartmentId { get; set; }

    public int PCId { get; set; }

    public SelectList? Departments { get; set; }
}