using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.Data.Models;
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
        var departments = UnitOfWork.DepartmentsRepository.GetAll();

        if (id == null)
        {
            return View(new UserEditViewModel()
            {
                Departments = new SelectList(
                    departments, 
                    nameof(Department.Id), 
                    nameof(Department.Name))
        });
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
            Departments = new SelectList(
                departments,
                nameof(Department.Id),
                nameof(Department.Name),
                user.DepartmentId)
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