using MatOrderingService.Domain;
using MatOrderingService.Domain.Models;
using MatOrderingService.Services.Storage.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Services.Storage
{
    public interface IOrderingService
    {
        Task<OrderInfo[]> GetAllOrders();
        Task<OrderInfo> Get(int id);
        Task<OrderInfo> Create(NewOrder order);
        Task<OrderInfo> Update(int id, EditOrder order);
        Task<bool> Delete(int id);
        Task<OrdersStaticticItem[]> GetStatistic();
        Task<OrdersStaticticItem[]> GetStatisticDapper();

    }
}
