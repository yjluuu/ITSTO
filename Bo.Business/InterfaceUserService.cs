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
    public class InterfaceUserService : BaseService, IInterfaceUserService
    {
        private readonly ILog _log;
        private readonly IRepository<InterfaceUser> interfaceUserService;
        public InterfaceUserService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(InterfaceUserService));
            this.interfaceUserService = this.CreateService<InterfaceUser>();
        }
        public InterfaceUser GetInterfaceUserByUser(InterfaceUser interfaceUser)
        {
            return interfaceUserService.Where(i => !i.IsDeleted && i.User == interfaceUser.User).FirstOrDefault();
        }
    }
}
