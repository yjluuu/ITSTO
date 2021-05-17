using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bo.Interface.IBusiness;

namespace Bo.Business
{
    public class InterfaceUserService : BaseService, IInterfaceUserService
    {
        public InterfaceUserService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }
        public InterfaceUser GetInterfaceUserByUser(InterfaceUser interfaceUser)
        {
            var interfaceUserService = this.CreateService<InterfaceUser>();
            return interfaceUserService.Where(i => !i.IsDeleted && i.User == interfaceUser.User).FirstOrDefault();
        }
    }
}
