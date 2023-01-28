using System.Data.SqlClient;
using TestAspNetMvc.Data.Models;
using TestAspNetMvc.Data.Repositories.Base;

namespace TestAspNetMvc.Data.Repositories;

public class UsersRepository : IUsersRepository
{
    private SqlConnection connection;

    public UsersRepository(SqlConnection connection)
    {
        this.connection = connection;
    }

    public User? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        string query = @"
SELECT U.Id, U.UserName, U.Salary, U.DepartmentId, U.PCId
FROM Users U
";
        List<User> users = new List<User>();
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    double salary = reader.GetDouble(2);
                    int departmentId = reader.GetInt32(3);
                    int pcId = reader.GetInt32(4);

                    var user = new User()
                    {
                        Id = id,
                        UserName = name,
                        Salary = salary,
                        DepartmentId = departmentId,
                        PCId = pcId
                    };

                    users.Add(user);
                }
            }
            connection.Close();
        }

        return users;
    }

    public User Add(User item)
    {
        throw new NotImplementedException();
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}