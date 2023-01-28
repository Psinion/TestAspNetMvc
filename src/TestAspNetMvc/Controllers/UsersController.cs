using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;

namespace TestAspNetMvc.Controllers;

public class UsersController : ConnectedController
{
    public UsersController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var allUSers = UnitOfWork.UsersRepository.GetAll();
        return View();
    }
}