#region usings -----------------------------------------------------------------

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

#endregion


namespace ApiTemplate.Controllers.v1
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    [EnableRateLimiting("FixedRateLimitWindow")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}   