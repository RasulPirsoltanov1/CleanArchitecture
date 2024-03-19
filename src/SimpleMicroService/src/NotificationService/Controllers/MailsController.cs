using Microsoft.AspNetCore.Mvc;
using NotificationService.Services;
using SimpleMicroService.Configurations.Models;
using System.Net;

namespace NotificationService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailsController : ControllerBase
{
    private readonly IMailService _mailService;
    public MailsController(IMailService mailService)
    {
        this._mailService = mailService;
    }


    [HttpPost]
    public async Task<IActionResult> Send(EmailBody request)
    {
        try
        {
            await _mailService.SendEmailAsync(request);
            return Ok(HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Ok(HttpStatusCode.BadRequest);
        }
    }
}


