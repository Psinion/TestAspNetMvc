using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Data.Repositories;

namespace TestAspNetMvc.Controllers.Base;

public class ConnectedController : Controller
{
    protected UnitOfWork UnitOfWork { get; set; }

    public ConnectedController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        UnitOfWork = new UnitOfWork(connectionString);
    }
}