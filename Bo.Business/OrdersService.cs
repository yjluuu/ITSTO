using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Transactions;
using log4net;
using Bo.Interface.IBusiness;

namespace Bo.Business
{
    public class OrdersService : BaseService, IOrdersService
    {
        private readonly ILog _log;
        private readonly IRepository<Orders> orderService;
        private readonly IRepository<OrderDetail> orderDetailService;
        public OrdersService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(OrdersService));
            this.orderService = this.CreateService<Orders>();
            this.orderDetailService = this.CreateService<OrderDetail>();
        }

        public bool NewOrders(Orders orders)
        {
            try
            {
                var orderInsertResult = orderService.Add(orders, false);
                orderDetailService.AddRange(orders.OrderDetailList.AsEnumerable<OrderDetail>(), false);
                if (orderInsertResult == null)
                {
                    _log.Error($"新增订单失败");
                    return false;
                }
                else
                {
                    orderService.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error($"新增订单详情异常：{ex.Message}");
                return false;
            }
            ////不通过SaveChanges方式，使用 TransactionScope 事务
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    var orderInsertResult = orderService.Add(orders);
            //    if (orderInsertResult != null)
            //    {
            //        try
            //        {
            //            orderDetailService.AddRange(orders.OrderDetailList.AsEnumerable<OrderDetail>());
            //            scope.Complete();
            //            return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            _log.Error($"新增订单详情异常：{ex.Message}");
            //            return false;
            //        }

            //    }
            //    _log.Error($"新增订单失败");
            //    return false;

            //}

        }
    }
}
