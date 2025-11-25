using Common;
using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController(ILogger<DiscountController> logger) : ControllerBase
{
    [HttpPost("calculate-discount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateDiscount([FromBody] Order order)
    {
        decimal discountInPercentage = 0.0m;

        if (order.Quantity >= 12)
        {
            discountInPercentage = 10.0m;
        }

        logger.LogInformation("Calculated discount is {Discount}%", discountInPercentage);

        return Ok(discountInPercentage);
    }
}
