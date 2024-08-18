#region usings -----------------------------------------------------------------

using ApiTemplate.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using ApiTemplate.Controllers.v1;
using ApiTemplate.Models;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ApiTemplateTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService = new Mock<IProductService>();
        private readonly Mock<ILogger<ProductsController>> _mockLogger = new Mock<ILogger<ProductsController>>();
        private readonly ProductsController _controller;

        public ProductControllerTests()
        {
            _controller = new ProductsController(_mockProductService.Object, _mockLogger.Object);
        }

        
    }
}
