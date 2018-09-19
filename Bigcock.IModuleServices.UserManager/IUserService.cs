using Bigcock.Data.Models;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bigcock.IModuleServices.UserManager
{
    [ServiceBundle("api/{Service}")]
    public interface IUserService :IServiceKey
    {
        Task<List<User>> GetUserAsync();

        Task<int> AddUser(User user);
    }
}
