using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IOrdersService
    {
        bool NewOrders(Orders orders);
        Orders GetOrderByOrderCode(string orderCode);
    }
}
