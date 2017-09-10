using MatOrderingService.Domain;
using MatOrderingService.Domain.Models;
using MatOrderingService.Exceptions;
using MatOrderingService.Services.Storage;
using MatOrderingService.Services.Storage.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private IOrderingService _ordersList;
        public OrdersController(IOrderingService ordersList)
        {
            _ordersList = ordersList;
        }

        [HttpGet]
        [Route("all")]
        [Route("/api/Orders")]
        
        public async Task<OrderInfo[]> Get([FromServices]IOrderingService ordersList)
        {
            var orders =  await ordersList.GetAllOrders();
            return orders;
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        /// <param name="id">ID of the order</param>
        /// <response code="200">Order found</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(void), 200)]
        public IActionResult Get(int id)
        {
            var order = _ordersList.Get(id);

            if (order == null)
                throw new EntityNotFoundException();

            return Ok(order);
        }

        [HttpGet("statistic")]
        [ProducesResponseType(typeof(OrdersStaticticItem[]),200)]
        public async Task<IActionResult> GetStatistic()
        {
            var ordersStatisicItems = await _ordersList.GetStatistic();

            return Ok(ordersStatisicItems);
        }

        [HttpGet("statistic/dapper")]
        [ProducesResponseType(typeof(OrdersStaticticItem[]), 200)]
        public async Task<IActionResult> GetStatisticDapper()
        {
            var ordersStatisticItems = await _ordersList.GetStatisticDapper();

            return Ok(ordersStatisticItems);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _ordersList.Delete(id);
            if (isDeleted)
                return Ok();
            throw new DivideByZeroException();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody]NewOrder order)
        {
            if (ModelState.IsValid)
            {
                _ordersList.Create(order);
                return Ok(order);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]EditOrder order)
        {
            await _ordersList.Update(id, order);
            return Ok(order);
        }
    }
}
