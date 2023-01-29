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
        string query = @"
SELECT 
      D.Id
    , D.Name
FROM Departments D
WHERE D.Id = @DEPARTMENT_ID
";

        Department department = null;
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", id);
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    department = ReadDepartment(reader);
                }
            }
            connection.Close();
        }

        return department;
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
        string query = @"
INSERT INTO Departments(Name)
VALUES (@NAME);
SELECT @@IDENTITY;";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("NAME", item.Name);

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

    public void Update(Department item)
    {
        string query = @"
UPDATE Departments
SET Name = @NAME
WHERE Id = @DEPARTMENT_ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("NAME", item.Name);
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", item.Id);

            cmd.Connection = connection;
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Remove(int id)
    {
        string query = @"
DELETE FROM Departments
WHERE Id = @DEPARTMENT_ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", id);

            cmd.Connection = connection;
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public bool HasUser(int id)
    {
        string query = @"
SELECT COUNT(D.Id)
FROM Departments D
JOIN Users U ON U.DepartmentId = D.Id
WHERE D.Id = @DEPARTMENT_ID;";

        bool hasUser = false;
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("DEPARTMENT_ID", id);

            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var count = reader.GetInt32(0);
                    if (count != 0)
                    {
                        hasUser = true;
                    }
                }
            }

            connection.Close();
        }

        return hasUser;
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