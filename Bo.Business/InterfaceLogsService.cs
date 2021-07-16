using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Business
{
    public class InterfaceLogsService : BaseService, IInterfaceLogsService
    {
        public InterfaceLogsService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }
        public bool LogInterface(InterfaceLogs logs)
        {
            var logService = this.CreateService<InterfaceLogs>();
            return logService.Add(logs).Id > 0;
        }
    }
}
