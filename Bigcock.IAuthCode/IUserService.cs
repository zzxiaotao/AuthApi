using Bigcock.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bigcock.IModuleServices.UserManager
{
    public interface IUserService
    {
        Task<List<User>> GetUserAsync();

        Task<int> AddUser(User user);
    }
}
