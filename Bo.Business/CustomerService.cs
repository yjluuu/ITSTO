using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using log4net;
using Newtonsoft.Json;
using Routine.Models.ApiEntityRequest;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Business
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ILog _log;
        private readonly IRepository<Customer> customerService;
        private readonly IRepository<CustomerChannel> customerChannelService;
        public CustomerService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(CustomerService));
            this.customerService = this.CreateService<Customer>();
            this.customerChannelService = this.CreateService<CustomerChannel>();
        }

        public string CustomerRegister(RequestCustomerRegister register)
        {
            var userCode = Guid.NewGuid().ToString("N");
            register.UserCode = userCode;
            _log.Info($"会员注册入参：{JsonConvert.SerializeObject(register)}");
            var customer = new Customer
            {
                Brand = register.Brand,
                UserCode = userCode,
                Mobile = register.Mobile,
                NickName = register.NickName,
                HeadImageUrl = register.HeadImageUrl,
                Gender = register.Gender,
                Age = register.Age,
                Birthday = register.Birthday,
                CreateUser = "yjl",
                CreateDate = DateTime.Now,
                LastUpdateUser = "yjl",
                LastUpdateDate = DateTime.Now
            };
            var customerChannel = new CustomerChannel
            {
                UserCode = userCode,
                UnionId = register.UnionId,
                OpenId = register.OpenId,
                SessionKey = register.SessionKey,
                CreateUser = "yjl",
                CreateDate = DateTime.Now,
                LastUpdateUser = "yjl",
                LastUpdateDate = DateTime.Now
            };
            var cr = customerService.Add(customer, false);
            if (cr == null)
            {
                _log.Info($"会员注册：往customer写入失败");
                return string.Empty;
            }
            var ccr = customerChannelService.Add(customerChannel, false);
            if (ccr == null)
            {
                _log.Info($"会员注册：往customerchannel写入失败");
                return string.Empty;
            }
            customerService.SaveChanges();
            return userCode;
        }


        public CustomerChannel GetCustomerChannelByUserCode(string userCode)
        {
            return customerChannelService.FirstOrDefault(cc => !cc.IsDeleted && cc.UserCode == userCode);
        }
    }
}
