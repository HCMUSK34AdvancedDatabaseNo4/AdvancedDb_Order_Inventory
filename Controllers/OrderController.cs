using AdvancedDb_Order_Inventory.Dtos;
using AdvancedDb_Order_Inventory.Models;
using AdvancedDb_Order_Inventory.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDb_Order_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderController(
            IOrderRepository orderRepository,
            IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrderHandleConcurrency([FromBody] List<OrderDto> orderDtos)
        {
            if (orderDtos == null || !orderDtos.Any())
            {
                return BadRequest(new ResponseMsg
                {
                    Status = 400,
                    Message = "Order data is required",
                    Data = null
                });
            }

            var (success, insufficientProducts) = await _orderRepository.PlaceOrderTransactionAsync(orderDtos);

            if (!success)
            {
                return Ok(new ResponseMsg
                {
                    Status = 400,
                    Message = "Place order failed, products stock is not enough.",
                    Data = insufficientProducts
                });
            }

            return Ok(new ResponseMsg
            {
                Status = 200,
                Message = "Order placed successfully",
                Data = null
            });
        }
    }
}