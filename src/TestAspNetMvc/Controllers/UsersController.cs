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
    [Route("Users/UserEdit")]
    [Route("Users/UserEdit/{id}")]
    public IActionResult UserEdit(UserEditViewModel userModel)
    {
        if (userModel.Salary < 0)
        {
            ModelState.AddModelError(nameof(userModel.Salary), "Введите положительное число");
        }

        if (userModel.UserName?.Length < 2)
        {
            ModelState.AddModelError(nameof(userModel.UserName), "Слишком короткое имя");
        }

        if (ModelState.IsValid)
        {
            var user = new User()
            {
                Id = userModel.Id,
                UserName = userModel.UserName,
                Salary = userModel.Salary ?? 0,
                DepartmentId = userModel.DepartmentId ?? 1,
                PCId = 1// userModel.PCId
            };

            if (user.Id == 0)
            {
                UnitOfWork.UsersRepository.Add(user);
            }
            else
            {
                UnitOfWork.UsersRepository.Update(user);
            }

            return RedirectToAction("Index");
        }

        var departments = UnitOfWork.DepartmentsRepository.GetAll();
        userModel.Departments = new SelectList(
            departments,
            nameof(Department.Id),
            nameof(Department.Name),
            userModel.DepartmentId);

        return View(userModel);
    }

    [HttpGet]
    [Route("Users/ChangePC/{id}/{searchString}")]
    [Route("Users/ChangePC/{id}")]
    public IActionResult ChangePC(int id, string searchString)
    {
        var user = UnitOfWork.UsersRepository.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        var pcs = UnitOfWork
            .PCRepository
            .GetAll();

        if (searchString != null && searchString != string.Empty)
        {
            pcs = pcs
                .Where(x => x.SearchString.Contains(searchString));
        }

        var userVm = new ChangePCViewModel()
        {
            Id = user.Id,
            SearchString = searchString,
            UserName = user.UserName,
            DepartmentName = user.Department.Name,
            PC = user.PC,
            FilteredPCs = pcs
        };

        return View(userVm);
    }

    [HttpGet]
    [Route("Users/SetPCToUser/{userId}/{pcId}")]
    public IActionResult SetPCToUser(int userId, int pcId)
    {
        var userRep = UnitOfWork.UsersRepository;

        var user = userRep.GetById(userId);
        if (user == null)
        {
            return NotFound();
        }

        user.PCId = pcId;
        userRep.Update(user);

        return RedirectToAction("Index");
    }
}