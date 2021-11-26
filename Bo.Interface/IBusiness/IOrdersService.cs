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
        Orders GetOrderByOrderCodeAndStatus(string orderCode, int status);
        void UpdateOrderStatusByOrderCode(Orders o);

        List<Orders> GetOrdersByUserCode(Orders param);
        List<OrderDetail> GetOrderDetailsByOrderCode(Orders param);
    }
}
