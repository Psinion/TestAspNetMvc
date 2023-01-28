using System.Data.SqlClient;
using TestAspNetMvc.Data.Models;
using TestAspNetMvc.Data.Repositories.Base;

namespace TestAspNetMvc.Data.Repositories;

public class PCRepository : IPCRepository
{
    private SqlConnection connection;

    public PCRepository(SqlConnection connection)
    {
        this.connection = connection;
    }

    public PC? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PC> GetAll()
    {
        throw new NotImplementedException();
    }
    public PC Add(PC item)
    {
        throw new NotImplementedException();
    }

    public void Update(PC item)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}