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
        string query = @"
SELECT 
      PC.Id
    , PC.Cpu
    , PC.Memory
    , PC.Hdd
FROM PC
WHERE PC.Id = @PC_ID
";

        PC pc = null;
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("PC_ID", id);
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    pc = ReadPC(reader);
                }
            }
            connection.Close();
        }

        return pc;
    }

    public IEnumerable<PC> GetAll()
    {
        string query = @"
SELECT 
      PC.Id
    , PC.Cpu
    , PC.Memory
    , PC.Hdd
FROM PC
";
        List<PC> pcs = new List<PC>();
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Connection = connection;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pc = ReadPC(reader);

                    pcs.Add(pc);
                }
            }
            connection.Close();
        }

        return pcs;
    }
    public PC Add(PC item)
    {
        string query = @"
INSERT INTO PC(Cpu, Memory, Hdd)
VALUES (@CPU, @MEMORY, @HDD);
SELECT @@IDENTITY;";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("CPU", item.Cpu);
            cmd.Parameters.AddWithValue("MEMORY", item.Memory);
            cmd.Parameters.AddWithValue("HDD", item.Hdd);

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

    public void Update(PC item)
    {
        string query = @"
UPDATE PC
SET Cpu = @CPU
  , Memory = @MEMORY
  , Hdd = @HDD
WHERE Id = @PC_ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("CPU", item.Cpu);
            cmd.Parameters.AddWithValue("MEMORY", item.Memory);
            cmd.Parameters.AddWithValue("HDD", item.Hdd);
            cmd.Parameters.AddWithValue("PC_ID", item.Id);

            cmd.Connection = connection;
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Remove(int id)
    {
        string query = @"
DELETE FROM PC
WHERE Id = @PC_ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("PC_ID", id);

            cmd.Connection = connection;
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public bool HasUser(int id)
    {
        string query = @"
SELECT COUNT(PC.Id)
FROM PC
JOIN Users U ON U.PCId = PC.Id
WHERE PC.Id = @PC_ID;";

        bool hasUser = false;
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("PC_ID", id);

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

    private PC ReadPC(SqlDataReader reader)
    {
        var id = reader.GetInt32(0);
        var cpu = reader.GetDouble(1);
        var memory = reader.GetDouble(2);
        var hdd = reader.GetDouble(3);

        var pc = new PC()
        {
            Id = id,
            Cpu = cpu,
            Memory = memory,
            Hdd = hdd,
        };

        return pc;
    }
}