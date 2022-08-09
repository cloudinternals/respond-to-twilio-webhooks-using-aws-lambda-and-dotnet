using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;

namespace TwilioWebhookLambda.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CallStatusChangeController : TwilioController
{
    private readonly ILogger<CallStatusChangeController> _logger;
    
    public CallStatusChangeController(ILogger<CallStatusChangeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task Index()
    {
        var form = await Request.ReadFormAsync();
        var to = form["To"];
        var callStatus = form["CallStatus"];
        var fromCountry = form["FromCountry"];
        var duration = form["Duration"];
        _logger.LogInformation(
            "Message to {to} changed to {callStatus}. (from country: {fromCountry}, duration: {duration})",
            to, callStatus, fromCountry, duration);
    }
}