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
    public class ChannelService : BaseService, IChannelService
    {
        private readonly ILog _log;
        private readonly IRepository<Channel> channelService;
        public ChannelService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(ChannelService));
            channelService = this.CreateService<Channel>();
        }

        public Channel GetChannelByBrand(string brand)
        {
            return channelService.Where(c => !c.IsDeleted && c.Brand == brand).FirstOrDefault();
        }
    }
}
