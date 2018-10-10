using Bigcock.Data.Models;
using Bigcock.IModuleServices.UserManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bigcock.AuthApiCore
{
    public class UserService : IUserService
    {
        private readonly UsersRepository _userRepostiory;
        public UserService(UsersRepository userRepostiory)
        {
            _userRepostiory = userRepostiory;
        }
        public Task<int> AddUser(User user)
        {
            return _userRepostiory.AddAsync(user);
        }

        public Task<List<User>> GetUserAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class UsersRepository
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(User user)
        {
            using (var db = new BigcockContext())
            {
                db.User.Add(user);
                return await db.SaveChangesAsync();
            }
        }
    }
}
