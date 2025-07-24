using Microsoft.AspNetCore.Mvc;
using Products.DAL.Models;
using Products.Service.Models;
using Products.Service.Operations.Interfaces;
using Products.Service.Utility;

namespace Products.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductOperations productOperations) : ControllerBase
    {
        private readonly IProductOperations _productOperations = productOperations;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> result = await _productOperations.GetAllProducts();
            return Ok(ApiResponse<List<Product>>.SuccessResponse(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            Product? result = await _productOperations.GetProduct(id);
            if (result == null)
            {
                return NotFound(ApiResponse<Product>.FailureMessage("Product Does Not Exist"));
            }
            return Ok(ApiResponse<Product>.SuccessResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductsDto product)
        {
            var result = await _productOperations.AddProduct(product);
            return CreatedAtAction(nameof(AddProduct),ApiResponse<Product>.SuccessResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductsDto product)
        {
            var result = await _productOperations.UpdateProduct(id, product);
            if (result == null)
                return NotFound(ApiResponse<Product>.FailureMessage("Product Not Found"));
            return Ok(ApiResponse<Product>.SuccessResponse(result));
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> UpdateStockDecement(long id,int quantity)
        {
            var (found, success, product) = await _productOperations.UpdateStockDecrement(id, quantity);
            if (!found || !success)
                return NotFound(ApiResponse<Product>.FailureMessage("Product Not Found"));
            return Ok(ApiResponse<Product>.SuccessResponse(product));
        }



        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> UpdateStockIncrement(long id, int quantity)
        {
            var result = await _productOperations.UpdateStockIncrement(id, quantity);
            if (result == null)
                return NotFound(ApiResponse<Product>.FailureMessage("Product Not Found"));
            return Ok(ApiResponse<Product>.SuccessResponse(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var result = await _productOperations.DeleteProduct(id);
            if (!result)
                return NotFound(ApiResponse<Product>.FailureMessage("Product Not Found"));
            return Ok(ApiResponse<bool>.SuccessResponse(result));
        }
    }
}
