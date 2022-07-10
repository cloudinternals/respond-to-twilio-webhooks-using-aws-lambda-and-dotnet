using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Twilio.AspNet.Core;

namespace TwilioWebhookSample.MinimalLambdaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CallStatusChangeController : TwilioController
{
    private readonly ILogger<CallStatusChangeController> _logger;
    
    public CallStatusChangeController(ILogger<CallStatusChangeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void Index()
    {
        var logObject = new
        {
            To = Request.Form["To"],
            CallStatus = Request.Form["CallStatus"],
            FromCountry = Request.Form["FromCountry"],
            Duration = Request.Form["Duration"]
        };
        var logMessage = JsonConvert.SerializeObject(logObject);
        _logger.LogInformation(logMessage);
    }
}