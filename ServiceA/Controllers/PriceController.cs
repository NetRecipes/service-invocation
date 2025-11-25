using Common;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PriceController(
    DaprClient daprClient,
    ILogger<PriceController> logger) : ControllerBase
{
    [HttpPost("calculate-price")]
    public async Task<IActionResult> Calculate([FromBody] Order order)
    {
        logger.LogInformation("Calculating discount for {Order}", order);

        var request = daprClient.CreateInvokeMethodRequest<Order>(
            HttpMethod.Post,
            "serviceb",
            "/api/discount/calculate-discount",
            [],
            order);

        var response = await daprClient.InvokeMethodAsync<decimal>(request);

        var totalPrice = order.PricePerUnit * order.Quantity;
        var discountedPrice = totalPrice - (totalPrice / 100.0m * response);
        return Ok(discountedPrice);
    }
}
