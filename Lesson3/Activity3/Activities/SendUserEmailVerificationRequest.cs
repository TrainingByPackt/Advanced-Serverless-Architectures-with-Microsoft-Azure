using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;

namespace VerifyUserEmail.Activities {
    public class SendUserEmailVerificationRequest {
        [FunctionName("SendUserEmailVerificationRequest")]
        public static void Run(
            [ActivityTrigger] (User,string) userInstanceTuple,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message
        ){
            var user = userInstanceTuple.Item1;
            var instanceId = userInstanceTuple.Item2;
            message = new SendGridMessage();
                message.AddTo(user.EmailAddress);
                message.SetFrom(new EmailAddress("EmailVerification@email.com"));
                message.AddContent("text/html",
                $@"<h1>Please Verify your email address</h1>
                <p><a href='http://localhost:7071/api/VerifyEmailAddress/{instanceId}'>Click here to Verify</a></p>");
                message.SetSubject("Order unsuccessful");
        }
    }
}