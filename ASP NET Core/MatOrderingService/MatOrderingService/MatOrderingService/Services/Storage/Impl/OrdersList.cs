using AutoMapper;
using Dapper;
using MatOrderingService.Domain;
using MatOrderingService.Domain.Models;
using MatOrderingService.Services.Storage.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static MatOrderingService.Domain.Models.Order;

namespace MatOrderingService.Services.Storage.Impl
{
    public class OrdersList : IOrderingService
    {
        private readonly IMapper _mapper;
        public readonly IHumanCodeGenerator _generator;
        private readonly OrdersDbContext _context;
        private static int nextId;

        public OrdersList(IMapper mapper, IHumanCodeGenerator generator, OrdersDbContext context)
        {
            _mapper = mapper;
            _generator = generator;
            _context = context;
            if (_context.Orders.OrderByDescending(o => o.Id).LastOrDefault() != null)
                nextId = _context.Orders.OrderByDescending(o => o.Id).LastOrDefault().Id;
            else nextId = 0;
        }

        public async Task<OrderInfo> Create(NewOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var currentId = ++nextId;
            var orderCode = await _generator.GetHumanCodeByID(currentId);


            var orderToAdd = new Order()
            {
                //Id = currentId,
                CreateDate = DateTime.Now,
                OrderCode = orderCode,
                CreatorId = order.CreatorId,
                OrderDetails = order.OrderDetails,
                IsDeleted = false,
                Status = OrderStatus.New
            };

            await _context.Orders.AddAsync(orderToAdd);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderInfo>(orderToAdd);
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderInfo> Get(int id)
        {
            var order = await _context.Orders
               .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            return _mapper.Map<OrderInfo>(order);
        }

        public async Task<OrderInfo[]> GetAllOrders()
        {
            var orders = await _context
              .Orders
            .AsNoTracking()
            .Where(p => !p.IsDeleted)
            .ToArrayAsync();

            return _mapper.Map<OrderInfo[]>(orders);
        }

        public async Task<OrdersStaticticItem[]> GetStatistic()
        {
            var ordersStatisticItems = await _context
                .Orders
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .GroupBy(g => g.CreatorId)
                .Select(p => new OrdersStaticticItem { CreatorId = p.Key, NumberOfOrders = p.Count() })
                .ToArrayAsync();

            return ordersStatisticItems;
        }

        public async Task<OrdersStaticticItem[]> GetStatisticDapper()
        {
            IEnumerable<OrdersStaticticItem> ordersStatisticItems;
            using (var connection = _context.Database.GetDbConnection())
            {
                ordersStatisticItems = await connection.QueryAsync<OrdersStaticticItem>(@"
                        SELECT CreatorId, COUNT(*) AS NumberOfOrders
                        FROM Orders
                        GROUP BY CreatorId;
                        ");
                return ordersStatisticItems.ToArray();
            }


            return new List<OrdersStaticticItem>().ToArray();

        }

        public async Task<OrderInfo> Update(int id, EditOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var orderInDB = await _context.Orders.FindAsync(id);
            var orderCode = orderInDB.OrderCode;
            var orderInfo = _mapper.Map<Order>(order);
            orderInfo.OrderCode = orderCode;
            orderInfo.Id = id;

            if (orderInDB != null)
            {
                _context.Orders.Remove(orderInDB);
                await _context.Orders.AddAsync(orderInfo);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<OrderInfo>(orderInfo);
        }
    }
}
