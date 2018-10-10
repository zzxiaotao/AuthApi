using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bigcock.Data.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bigcock.AuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            
            //在启动的时候吧连接字符串赋值
            BigcockContext.ConnectionString = "{SqlServerStr}|Server=.;Database=Bigcock;User ID=sa;Password=123456;Trusted_Connection=False;";

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) => {
                config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
            })
                .UseStartup<Startup>();
    }
}
