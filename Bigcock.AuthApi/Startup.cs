using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bigcock.Data.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bigcock.AuthApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IContainer AutofacContainer;
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            #region Auth 
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = JwtClaimTypes.Name,
                       RoleClaimType = JwtClaimTypes.Role,

                       ValidIssuer = "http://localhost:44319",
                       ValidAudience = "api",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is a security key"))
                       /***********************************TokenValidationParameters的参数默认值***********************************/
                       // RequireSignedTokens = true,
                       // SaveSigninToken = false,
                       // ValidateActor = false,
                       // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                       // ValidateAudience = true,
                       // ValidateIssuer = true, 
                       // ValidateIssuerSigningKey = false,
                       // 是否要求Token的Claims中必须包含Expires
                       // RequireExpirationTime = true,
                       // 允许的服务器时间偏移量
                       // ClockSkew = TimeSpan.FromSeconds(300),
                       // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                       // ValidateLifetime = true
                   };
               });
            #endregion

            services.AddDbContext<BigcockContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BigcockContext")));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterModule<DefaultModuleRegister>();
            AutofacContainer = containerBuilder.Build();
            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            appLifetime.ApplicationStopped.Register(()=> { AutofacContainer.Dispose(); });
        }
        public class DefaultModuleRegister : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //注册当前程序集中以“Ser”结尾的类,暴漏类实现的所有接口，生命周期为PerLifetimeScope
                builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
                builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
                //注册所有"MyApp.Repository"程序集中的类
                //builder.RegisterAssemblyTypes(GetAssembly("MyApp.Repository")).AsImplementedInterfaces();
            }

            public static System.Reflection.Assembly GetAssembly(string assemblyName)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
                return assembly;
            }
        }
    }
}
