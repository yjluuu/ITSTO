using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Business
{
    public class AppSettingService : BaseService, IAppSettingService
    {
        public AppSettingService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }

        public IQueryable<AppSetting> GetAppSettingByKey(string brand, string appSettingKey)
        {
            var appSettingService = this.CreateService<AppSetting>();
            return appSettingService.Where(t => !t.IsDeleted && t.Brand == brand && t.AppSettingKey == appSettingKey);
        }
    }
}
