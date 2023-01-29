using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.ViewModels;

namespace TestAspNetMvc.Controllers;

public class DepartmentsController : ConnectedController
{
    public DepartmentsController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var usersVm = new DepartmentsViewModel()
        {
            Departments = UnitOfWork.DepartmentsRepository.GetAll()
        };
        return View(usersVm);
    }
}