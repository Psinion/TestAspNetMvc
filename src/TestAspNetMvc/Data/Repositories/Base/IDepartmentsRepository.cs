using TestAspNetMvc.Data.Models;

namespace TestAspNetMvc.Data.Repositories.Base;

public interface IDepartmentsRepository : IRepository<Department>
{
    public bool HasUser(int id);
}