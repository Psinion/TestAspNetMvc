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