using Common.Tool;
using ITSTOAPI.Controllers;
using ITSTOAPI.Attribute;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //���ݿ�����
            //DBContext.conStr = Configuration.GetConnectionString("connStr");
            services.AddDbContext<DBContext>(options => { options.UseMySql(Configuration.GetConnectionString("connStr")); }, ServiceLifetime.Scoped);
            //redis����
            RedisClient.redisClient.InitConnect(Configuration);
            //���ȫ�ֵ�Authorization������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomAuthorization)); });
            //���ȫ�ֵ�Action������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomActionFilterAttribute)); });
            //���ȫ�ֵ�Exception������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomExceptionFilterAttribute)); });
            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperConfigures));

            #region ע��
            services.AddTransient<Bo.Interface.IRepository.IRepositoryFactory, Bo.Repository.RepositoryFactory>();
            services.AddTransient<Bo.Interface.IBusiness.ITestService, Bo.Business.TestService>();
            services.AddTransient<Bo.Interface.IBusiness.IStoreService, Bo.Business.StoreService>();
            services.AddTransient<Bo.Interface.IBusiness.IInterfaceUserService, Bo.Business.InterfaceUserService>();
            services.AddTransient<Bo.Interface.IBusiness.IInterfaceMappingService, Bo.Business.InterfaceMappingService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "api/{controller}/{action}");
            });

        }
    }
}
