using TestAspNetMvc.Data.Models;

namespace TestAspNetMvc.Data.Repositories.Base;

public interface IPCRepository : IRepository<PC>
{
    public bool HasUser(int id);
}