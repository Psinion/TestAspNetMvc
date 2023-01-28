using System.Data.SqlClient;
using TestAspNetMvc.Data.Repositories.Base;

namespace TestAspNetMvc.Data.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private SqlConnection connection;

    public IUsersRepository UsersRepository { get; }
    public IDepartmentsRepository DepartmentsRepository { get; }
    public IPCRepository PCRepository { get; }

    public UnitOfWork(string connectionString)
    {
        connection = new SqlConnection(connectionString);

        UsersRepository = new UsersRepository(connection);
        DepartmentsRepository = new DepartmentsRepository(connection);
        PCRepository = new PCRepository(connection);
    }

    public void Dispose()
    {
        connection.Dispose();
    }
}