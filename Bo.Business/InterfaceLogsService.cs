using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using log4net;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Business
{
    public class InterfaceLogsService : BaseService, IInterfaceLogsService
    {
        private readonly ILog _log;
        private readonly IRepository<InterfaceLogs> logService;
        public InterfaceLogsService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(InterfaceLogsService));
            this.logService = this.CreateService<InterfaceLogs>();
        }
        public bool LogInterface(InterfaceLogs logs)
        {
            return logService.Add(logs).Id > 0;
        }
    }
}
