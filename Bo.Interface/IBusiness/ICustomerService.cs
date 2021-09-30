using Routine.Models.ApiEntityRequest;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface ICustomerService
    {
        string CustomerRegister(RequestCustomerRegister register);
        CustomerChannel GetCustomerChannelByUserCode(string userCode);
        CustomerChannel GetCustomerChannelByOpenId(string openId);
        Customer GetCustomerByUserCode(string userCode);

        void UpdateCustomerByUserCode(Customer c);

    }
}
