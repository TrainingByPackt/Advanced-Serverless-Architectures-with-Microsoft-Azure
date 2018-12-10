using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace VerifyUserEmail.EventEmitters {
    public class VerifyEmailAddress {
        [FunctionName("VerifyEmailAddress")]
        public static async void Run([HttpTrigger(
                AuthorizationLevel.Function,
                "get",
                Route = "VerifyEmailAddress/{instanceId}")] HttpRequest request,
            string instanceId,
            [OrchestrationClient] DurableOrchestrationClientBase client)
            {
                await client.RaiseEventAsync(instanceId, "EmailVerified",true);
            }
    }
}