using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.ViewModels;

namespace TestAspNetMvc.Controllers;

public class PCController : ConnectedController
{
    public PCController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var usersVm = new PCViewModel()
        {
            PCs = UnitOfWork.PCRepository.GetAll()
        };
        return View(usersVm);
    }
}