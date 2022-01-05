using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _sqlService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository dataAccessRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Order>> GetAllOrders()
        {
            List<Order> orders = _sqlService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost, Authorize]
        public ActionResult<Order> CreateOrder(OrderCreateVM orderCreateVM)
        {
            var products = orderCreateVM.Products;
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            Order order = new Order()
            {
                CreatedDate = DateTime.Now,
                DeliveryAddress = orderCreateVM.DeliveryAddress,
                MobileNo = orderCreateVM.MobileNo,
                CustomerId = UserId,
                OrderStatus = OrderStatus.Placed,
                TotalAmount = products.Sum(p => p.Price) + 200 //+ delivery fee
            };

            var orderCreated = _sqlService.SaveOrder(order);
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();

            foreach (var product in products)
            {
                orderDetailsList.Add(new OrderDetail
                {
                    CreatedDate = DateTime.Now,
                    Amount = product.Price,
                    OrderId = orderCreated.Id,
                    ProductId = product.Id,
                });
            }
            orderCreated.OrderDetails = orderDetailsList;
            _sqlService.UpdateOrder(orderCreated);

            return Created("Orders", orderCreated);
        }
    }
}
