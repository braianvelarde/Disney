using SendGrid;
using SendGrid.Helpers.Mail;

namespace Disney.Services.MailService
{
    public class SendEmail : ISendEmail
    {
        async Task ISendEmail.SendEmail(string email, string userName)
        {
            var client = new SendGridClient(
                "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");
            var from = new EmailAddress("braianvelarde94@hotmail.com", "Braian Velarde");
            var subject = "Welcome to Alkemy Disney!";
            var to = new EmailAddress(email, userName);
            var textContent = "Hello!";
            var htmlContent = "<strong>Disney api challenge</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, textContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
