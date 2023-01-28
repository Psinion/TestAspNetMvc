using TestAspNetMvc.Data.Models.Base;

namespace TestAspNetMvc.Data.Models;

public class PC : Entity
{
    public double Cpu { get; set; }
    public double Memory { get; set; }
    public double Hdd { get; set; }
}