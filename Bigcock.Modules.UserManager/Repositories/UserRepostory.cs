using Bigcock.Data.Models;
using Microsoft.EntityFrameworkCore;
using Surging.Core.CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigcock.Modules.UserManager.Repositories
{
    public class UserRepostory : BaseRepository
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(User user)
        {
            using (var db =new BigcockContext())
            {
                db.User.Add(user);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            using (var db = new BigcockContext())
            {
                return db.User.AsNoTracking().ToList();
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using (var db = new BigcockContext())
            {
                return await db.User.AsNoTracking().ToListAsync();
            }
        }
    }
}
