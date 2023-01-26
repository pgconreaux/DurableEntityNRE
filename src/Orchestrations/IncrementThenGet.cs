using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

public static class IncrementThenGet
{
    [FunctionName(nameof(IncrementThenGet))]
    public static async Task Run(
        [OrchestrationTrigger] IDurableOrchestrationContext context)
    {
        var entityId = new EntityId("Counter", "myCounter");

        context.SignalEntity(entityId, "Add", 1);

        await context.CallSubOrchestratorAsync(nameof(GetEntityAsync), entityId);
    }

    [FunctionName(nameof(GetEntityAsync))]
    public static async Task GetEntityAsync(
    [OrchestrationTrigger] IDurableOrchestrationContext context)
    {
        var entityId = context.GetInput<EntityId>();

        _ = await context.CallEntityAsync<int>(entityId, "Get");
    }

}
