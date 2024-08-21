namespace IdentityService.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string recipientEmail, string subject, object body);


    }
}
