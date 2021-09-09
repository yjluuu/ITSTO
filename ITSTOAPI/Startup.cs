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
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace ITSTOAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //���ݿ�����
            //DBContext.conStr = Configuration.GetConnectionString("connStr");
            services.AddDbContext<DBContext>(options => { options.UseMySql(Configuration.GetConnectionString("connStr")); }, ServiceLifetime.Scoped);
            //redis����
            RedisClient.redisClient.InitConnect(Configuration);
            services.AddControllers(options => { options.Filters.Add(typeof(CustomResourceAttribute)); });
            //���ȫ�ֵ�Authorization������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomAuthorization)); });
            //���ȫ�ֵ�Action������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomActionFilterAttribute)); });
            //���ȫ�ֵ�Exception������
            services.AddControllers(options => { options.Filters.Add(typeof(CustomExceptionFilterAttribute)); });
            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperConfigures));
            //���httpcontext
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                ////��������Ĭ�ϸ�ʽ������ ��ʽ1
                //options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });
                ////��������Ĭ�ϸ�ʽ������ ��ʽ2
                //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                //options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                //��ֵ����
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            #region ע��
            services.AddTransient<Bo.Interface.IBusiness.IInterfaceLogsService, Bo.Business.InterfaceLogsService>();
            services.AddTransient<Bo.Interface.IRepository.IRepositoryFactory, Bo.Repository.RepositoryFactory>();
            services.AddTransient<Bo.Interface.IBusiness.ITestService, Bo.Business.TestService>();
            services.AddTransient<Bo.Interface.IBusiness.IStoreService, Bo.Business.StoreService>();
            services.AddTransient<Bo.Interface.IBusiness.IInterfaceUserService, Bo.Business.InterfaceUserService>();
            services.AddTransient<Bo.Interface.IBusiness.IInterfaceMappingService, Bo.Business.InterfaceMappingService>();
            services.AddTransient<Bo.Interface.IBusiness.IDishCategoryService, Bo.Business.DishCategoryService>();
            services.AddTransient<Bo.Interface.IBusiness.IDishService, Bo.Business.DishService>();
            services.AddTransient<Bo.Interface.IBusiness.IAppSettingService, Bo.Business.AppSettingService>();
            services.AddTransient<Bo.Interface.IBusiness.IOrdersService, Bo.Business.OrdersService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });


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
