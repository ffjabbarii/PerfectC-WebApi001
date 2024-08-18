#region usings -----------------------------------------------------------------

using ApiTemplate.Services;
using Microsoft.Extensions.Logging;
using Moq;
using ApiTemplate.Models;

#endregion

namespace ApiTemplateTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<ILogger<ProductService>> _mockLogger = new Mock<ILogger<ProductService>>();
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService(_mockLogger.Object);
        }

        
    }
}
