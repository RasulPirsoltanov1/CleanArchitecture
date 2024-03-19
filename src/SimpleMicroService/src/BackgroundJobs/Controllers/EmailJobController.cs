using BackgroundJobs.Models;
using BackgroundJobs.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace BackgroundJobs.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class EmailJobController : ControllerBase
{
    #region Constructor
    private readonly IBackgroundJobClient _client;
    public EmailJobController(IBackgroundJobClient client)
    {
        this._client = client;
    }
    #endregion

    #region Send Email
    [HttpPost("sendEmail")]
    public IActionResult SendEmail([FromBody] EmailRequest request)
    {

        if (request.SendNow)
        {
            _client.Enqueue(() => SendEmailJob(request));
        }
        else
        {
            var timeUntilSendDate = request.SendDate.Value - DateTime.Now;
            _client.Schedule(() => SendEmailJob(request), timeUntilSendDate);
        }


        //_client.Enqueue(() => SendEmailJob(request));
        //_client.Schedule(() => SendEmailJob(request), TimeSpan.FromMinutes(15));
        //for (int i = 0; i < 10; i++)
        //{
        //    _client.Schedule(() => SendEmailJob(request), TimeSpan.FromSeconds(10 + i));
        //}
        return Ok("E-posta göndermi planlandı");
    }
    #endregion

    [NonAction]
    public async Task SendEmailJob(EmailRequest request)
    {
        var mailServiceApi = RestService.For<IMailServiceApi>("http://localhost:5116");
        await mailServiceApi.SendEmailAsync(request);
    }
}
