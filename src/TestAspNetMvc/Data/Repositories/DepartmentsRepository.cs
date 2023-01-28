using System.Data.SqlClient;
using TestAspNetMvc.Data.Models;
using TestAspNetMvc.Data.Repositories.Base;

namespace TestAspNetMvc.Data.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private SqlConnection connection;

    public DepartmentsRepository(SqlConnection connection)
    {
        this.connection = connection;
    }

    public Department? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Department> GetAll()
    {
        throw new NotImplementedException();
    }

    public Department Add(Department item)
    {
        throw new NotImplementedException();
    }

    public void Update(Department item)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}