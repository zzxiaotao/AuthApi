using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Bigcock.AuthApiCore;
using Bigcock.Data.Models;
using Bigcock.IModuleServices.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bigcock.AuthApi.Controllers
{
    //[Authorize] 
    public class ValuesController : ControllerBase
    {
        private IUserService _user;
        public ValuesController(IUserService user)
        {
            _user = user;
        }
        // GET api/values
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var user = new User
            {
                CreateTime = DateTime.Now,
                Id = 1,
                Name = "",
                Sex = "男"
            };
            using (var scope = Startup.AutofacContainer.BeginLifetimeScope())
            {
                IConfiguration config = scope.Resolve<IConfiguration>();
                IHostingEnvironment env = scope.Resolve<IHostingEnvironment>();
            }
            _user.AddUser(user);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        

    }
}
