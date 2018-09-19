using Bigcock.Data.Models;
using Bigcock.IModuleServices.UserManager;
using Bigcock.Modules.UserManager.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bigcock.Modules.UserManager.Domain
{
    public class UserService : ProxyServiceBase, IUserService
    {
        private readonly UserRepostory _userRepostiory;
        public UserService(UserRepostory userRepostiory)
        {
            _userRepostiory = userRepostiory;
        }
        public Task<int> AddUser(User user)
        {
            return _userRepostiory.AddAsync(user);
        }

        public Task<List<User>> GetUserAsync()
        {
            return _userRepostiory.GetUsersAsync();
        }
    }
}
