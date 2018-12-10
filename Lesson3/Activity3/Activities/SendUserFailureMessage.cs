using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;

namespace VerifyUserEmail.Activities {
    public class SendUserFailureMessage {
        [FunctionName("SendUserFailureMessage")]
        public static void Run(
            [ActivityTrigger] User user,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message
        ){
            message = new SendGridMessage();
                message.AddTo(user.EmailAddress);
                message.SetFrom(new EmailAddress("EmailVerification@email.com"));
                message.AddContent("text/html",
                $@"<h1>Email Was not verified</h1>");
                message.SetSubject("Email verification failure");
        }
    }
}