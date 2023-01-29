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

    public User? GetById(int userId)
    {
        string query = @"
SELECT 
      U.Id
    , U.UserName
    , U.Salary
    , D.Id
    , D.Name
    , PC.Id
    , PC.Cpu
    , PC.Memory
    , PC.Hdd
FROM Users U
JOIN Departments D ON D.Id = U.DepartmentId
JOIN PC PC ON PC.Id = U.PCId
WHERE U.Id = @USER_ID
";

        User user = null;
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("USER_ID", userId);
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = ReadUser(reader);
                }
            }
            connection.Close();
        }

        return user;
    }

    public IEnumerable<User> GetAll()
    {
        string query = @"
SELECT 
      U.Id
    , U.UserName
    , U.Salary
    , D.Id
    , D.Name
    , PC.Id
    , PC.Cpu
    , PC.Memory
    , PC.Hdd
FROM Users U
JOIN Departments D ON D.Id = U.DepartmentId
JOIN PC PC ON PC.Id = U.PCId
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
                    var user = ReadUser(reader);

                    users.Add(user);
                }
            }
            connection.Close();
        }

        return users;
    }

    public User Add(User item)
    {
        string query = @"
INSERT INTO Users(UserName, Salary, DepartmentId, PCId)
VALUES (@USER_NAME, @SALARY, @DEPARTMENT_ID, @PC_ID);
SELECT @@IDENTITY;";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("USER_NAME", item.UserName);
            cmd.Parameters.AddWithValue("SALARY", item.Salary);
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", item.DepartmentId);
            cmd.Parameters.AddWithValue("PC_ID", item.PCId);

            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetDecimal(0);
                    item.Id = (int)id;
                }
            }

            connection.Close();
        }

        return item;
    }

    public void Update(User item)
    {
        string query = @"
UPDATE Users
SET UserName = @USER_NAME
  , Salary = @SALARY
  , DepartmentId = @DEPARTMENT_ID
  , PCId = @PC_ID
WHERE Id = @USER_ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("USER_NAME", item.UserName);
            cmd.Parameters.AddWithValue("SALARY", item.Salary);
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", item.DepartmentId);
            cmd.Parameters.AddWithValue("PC_ID", item.PCId);
            cmd.Parameters.AddWithValue("USER_ID", item.Id);

            cmd.Connection = connection;
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    private User ReadUser(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var name = reader.GetString(1);
        var salary = reader.GetDouble(2);
        var departmentId = reader.GetInt32(3);
        var departmentName = reader.GetString(4);
        var pcId = reader.GetInt32(5);
        var pcCpu = reader.GetDouble(6);
        var pcMemory = reader.GetDouble(7);
        var pcHdd = reader.GetDouble(8);

        var department = new Department()
        {
            Id = departmentId,
            Name = departmentName
        };

        var pc = new PC()
        {
            Id = pcId,
            Cpu = pcCpu,
            Memory = pcMemory,
            Hdd = pcHdd
        };

        var user = new User()
        {
            Id = id,
            UserName = name,
            Salary = salary,
            DepartmentId = departmentId,
            Department = department,
            PCId = pcId,
            PC = pc
        };

        return user;
    }
}