#region usings -----------------------------------------------------------------

using ApiTemplate.Models;
using ApiTemplate.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

#endregion

namespace ApiTemplate.Controllers.v1
{
    // [Authorize] // Uncomment if you configured JWT authentication and AuthController
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    [EnableRateLimiting("FixedRateLimitWindow")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                IEnumerable<Product?> products = await _productService.GetAllProducts();

                if (products == null)
                {
                    _logger.LogError("Failed to retrieve products.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid product ID.");
                }

                Product? product = await _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product with ID: {ID}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the product.");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for the email content.");
                    return BadRequest(ModelState);
                }

                bool success = await _productService.AddProduct(product);

                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the product.");
                }

                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the product.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for the email content.");
                    return BadRequest(ModelState);
                }

                bool success = await _productService.UpdateProduct(product);

                if (!success)
                {
                    return NotFound($"Product with ID {product.Id} not found or an error occurred while updating.");
                }

                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product with ID: {ID}.", product.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid product ID.");
                }

                bool success = await _productService.DeleteProductById(id);

                if (!success)
                {
                    return NotFound($"Product with ID {id} not found or an error occurred while deleting.");
                }

                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product with ID: {ID}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product.");
            }
        }
    }
}


