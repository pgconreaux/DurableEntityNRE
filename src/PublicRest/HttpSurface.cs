using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

public static class HttpSurface
{
    [FunctionName(nameof(TestGet))]
    public static async Task<IActionResult> TestGet(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test")] HttpRequest req,
      [DurableClient] IDurableClient client,
      ILogger log)
    {
        var instanceId = await client.StartNewAsync(nameof(IncrementThenGet));
        return client.CreateCheckStatusResponse(req, instanceId);
    }
}
