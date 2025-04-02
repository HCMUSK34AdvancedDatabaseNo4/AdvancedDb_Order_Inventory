using AdvancedDb_Order_Inventory.Dtos;
using AdvancedDb_Order_Inventory.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDb_Order_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet("GetAvailableProduct")]
        public async Task<IActionResult> GetAvailableProduct()
        {
            var availableProducts = await _inventoryRepository.GetAvailableProductsAsync();

            if (availableProducts == null || !availableProducts.Any())
            {
                return Ok(new ResponseMsg
                {
                    Status = 404,
                    Message = "No available products found.",
                    Data = null
                });
            }

            return Ok(new ResponseMsg
            {
                Status = 200,
                Message = "Available products retrieved successfully.",
                Data = availableProducts
            });
        }

        [HttpGet("GetProductQuantity")]
        public async Task<IActionResult> GetProductQuantity(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest(new ResponseMsg
                {
                    Status = 400,
                    Message = "Product ID is required",
                    Data = null
                });
            }

            var product = await _inventoryRepository.GetProductQuantityAsync(productId);

            if (product == null)
            {
                return Ok(new ResponseMsg
                {
                    Status = 404,
                    Message = "Get product quantity failed, product does not exist",
                    Data = null
                });
            }

            return Ok(new ResponseMsg
            {
                Status = 200,
                Message = "Get product quantity success",
                Data = product
            });
        }
    }
}
