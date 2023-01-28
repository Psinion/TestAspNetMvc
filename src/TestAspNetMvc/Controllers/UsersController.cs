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
}