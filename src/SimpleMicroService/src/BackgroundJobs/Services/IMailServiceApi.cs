using BackgroundJobs.Models;
using Refit;

namespace BackgroundJobs.Services;

public interface IMailServiceApi
{
    [Post("/api/mails")]
    Task SendEmailAsync([Body] EmailRequest emailRequest);
}
