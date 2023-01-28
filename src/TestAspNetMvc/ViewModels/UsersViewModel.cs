using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels.Base;

namespace TestAspNetMvc.ViewModels
{
    public class UsersViewModel : IBaseViewModel
    {
        public string Title => "Пользователи";
        public IEnumerable<User> Users { get; set; }
    }
}
