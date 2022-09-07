namespace Disney.Services.MailService
{
    public interface ISendEmail
    {
        Task SendEmail(string email, string userName);
    }
}
