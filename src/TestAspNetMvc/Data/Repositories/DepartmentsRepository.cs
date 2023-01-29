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
        string query = @"
SELECT 
      D.Id
    , D.Name
FROM Departments D
";
        List<Department> departments = new List<Department>();
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = ReadDepartment(reader);

                    departments.Add(user);
                }
            }
            connection.Close();
        }

        return departments;
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

    private Department ReadDepartment(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var name = reader.GetString(1);

        var department = new Department()
        {
            Id = id,
            Name = name
        };

        return department;
    }
}