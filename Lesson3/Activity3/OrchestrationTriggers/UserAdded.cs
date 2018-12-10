using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace VerifyUserEmail.OrchestrationTriggers
{
    public static class UserAdded
    {
        [FunctionName("UserAdded")]
        public static void Run([CosmosDBTrigger(
            databaseName: "serverless",
            collectionName: "users",
            ConnectionStringSetting = "AzureWebJobsStorage",
            LeaseCollectionName = "leases")]IReadOnlyList<User> user,
            [OrchestrationClient] DurableOrchestrationClientBase orchestrationClientBase, ILogger log)
        {
            orchestrationClientBase.StartNewAsync("OrchestrateVerifyUserEmailWorkflow",user);
        }
    }
}
