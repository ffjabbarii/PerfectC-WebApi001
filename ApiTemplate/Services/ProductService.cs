#region usings -----------------------------------------------------------------

using ApiTemplate.Models;
using ApiTemplate.Services.Interfaces;

#endregion

// NOTE: THIS IS AN EXAMPLE SERVICE IMPLEMENTATION.
// PRODUCTION SERVICES SHOULD USE A DATABASE OR OTHER PERSISTENT STORAGE MECHANISM.
// THIS SERVICE IS FOR DEMONSTRATION PURPOSES ONLY.

namespace ApiTemplate.Services
{
    public class ProductService: IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.00m },
            new Product { Id = 2, Name = "Product 2", Price = 20.00m },
            new Product { Id = 3, Name = "Product 3", Price = 30.00m }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Product?>> GetAllProducts()
        {
            try
            {
                return await Task.FromResult(_products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all products.");
                return await Task.FromResult<IEnumerable<Product?>>(null);
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                Product? product = _products.FirstOrDefault(p => p.Id == id);
                return await Task.FromResult(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product with ID: {ID}.", id);
                return await Task.FromResult<Product?>(null);
            }

        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                product.Id = _products.Count + 1;
                _products.Add(product);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new product.");
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                Product? existingProduct = await GetProductById(product.Id);
                if (existingProduct == null)
                {
                    return await Task.FromResult(false);
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product with ID: {ID}.", product.Id);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteProductById(int id)
        {
            try
            {
                Product? product = await GetProductById(id);
                if (product == null)
                {
                    return await Task.FromResult(false);
                }

                _products.Remove(product);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product with ID: {ID}.", id);
                return await Task.FromResult(false);
            }
        }
    }
}
