namespace TestAspNetMvc.Data.Repositories.Base;

public interface IUnitOfWork : IDisposable
{
    IUsersRepository UsersRepository { get; }
    IDepartmentsRepository DepartmentsRepository { get; }
    IPCRepository PCRepository { get; }
}