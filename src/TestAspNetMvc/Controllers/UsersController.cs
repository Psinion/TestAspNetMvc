using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.ViewModels;

namespace TestAspNetMvc.Controllers;

public class UsersController : ConnectedController
{
    public UsersController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var usersVm = new UsersViewModel()
        {
            Users = UnitOfWork.UsersRepository.GetAll()
        };
        return View(usersVm);
    }

    [HttpGet]
    [Route("Users/UserEdit")]
    [Route("Users/UserEdit/{id}")]
    public IActionResult UserEdit(int? id)
    {
        if (id == null)
        {
            return View(new UserEditViewModel());
        }

        var user = UnitOfWork.UsersRepository.GetById(id.Value);
        if (user == null)
        {
            return NotFound();
        }

        var userVm = new UserEditViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            Salary = user.Salary,
            DepartmentId = user.DepartmentId,
            PCId = user.PCId,
        };

        return View(userVm);
    }

    [HttpPost]
    public IActionResult UserEdit(UserEditViewModel model)
    {
        if (model.Salary < 0)
        {
            ModelState.AddModelError(nameof(model.Salary), "Введите положительное число");
        }

        if (model.UserName?.Length < 2)
        {
            ModelState.AddModelError(nameof(model.UserName), "Слишком короткое имя");
        }

        if (ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        return View(model);
    }
}