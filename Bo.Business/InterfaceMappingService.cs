using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bo.Interface.IBusiness;
using log4net;

namespace Bo.Business
{
    public class InterfaceMappingService : BaseService, IInterfaceMappingService
    {
        private readonly ILog _log;
        private readonly IRepository<InterfaceMapping> interfaceMappingService;
        public InterfaceMappingService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(InterfaceMappingService));
            this.interfaceMappingService = this.CreateService<InterfaceMapping>();
        }

        public IQueryable<InterfaceMapping> GetInterfaceMappingByUser(InterfaceMapping interfaceMapping)
        {
            return interfaceMappingService.Where(i => !i.IsDeleted && i.Brand == interfaceMapping.Brand && i.InterfaceUserId == interfaceMapping.InterfaceUserId);
        }
    }
}
