using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace VerifyUserEmail.Orchestrators {
    public class OrchestrateVerifyUserEmailWorkflow {
        [FunctionName("OrchestrateVerifyUserEmailWorkflow")]
        public static async void Run([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var user = context.GetInput<User>();
            await context.CallActivityAsync("SendUserEmailVerificationRequest",(user,context.InstanceId));
                 
            DateTime deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromMinutes(30));
            Task timerTask = context.CreateTimer(deadline, CancellationToken.None);
            Task emailVerifiedTask = context.WaitForExternalEvent<bool>("EmailVerified");
            Task winningTask = await Task.WhenAny(timerTask, emailVerifiedTask);
            if(winningTask == emailVerifiedTask){
                await context.CallActivityAsync("SendUserSuccessMessage",user);
            } else {
                await context.CallActivityAsync("SendUserFailureMessage",user);
            }
        }
    }
}