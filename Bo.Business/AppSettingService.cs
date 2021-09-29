using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using log4net;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Business
{
    public class AppSettingService : BaseService, IAppSettingService
    {
        private readonly ILog _log;
        private readonly IRepository<AppSetting> appSettingService;
        public AppSettingService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(AppSettingService));
            this.appSettingService = this.CreateService<AppSetting>();
        }

        public AppSetting GetAppSettingByKey(string brand, string appSettingKey)
        {
            return appSettingService.Where(t => !t.IsDeleted && t.Brand == brand && t.AppSettingKey == appSettingKey).FirstOrDefault();
        }
    }
}
