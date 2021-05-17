using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bo.Interface.IBusiness;

namespace Bo.Business
{
    public class InterfaceMappingService : BaseService, IInterfaceMappingService
    {
        public InterfaceMappingService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }

        public IQueryable<InterfaceMapping> GetInterfaceMappingByUser(InterfaceMapping interfaceMapping)
        {
            var interfaceMappingService = this.CreateService<InterfaceMapping>();
            return interfaceMappingService.Where(i => !i.IsDeleted && i.Brand == interfaceMapping.Brand && i.InterfaceUserId == interfaceMapping.InterfaceUserId);
        }
    }
}
