using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;
 
namespace TwilioWebhookLambda.WebApi.Controllers;
 
[ApiController]
[Route("api/[controller]")]
public class IncomingCallController : TwilioController
{
    [HttpPost]
    public TwiMLResult Index()
    {
        var response = new VoiceResponse();
        response.Say("Hello. Please leave a message after the beep.");
        response.Record(
            timeout: 10, 
            recordingStatusCallback: new Uri("/api/RecordingStatusChange", UriKind.Relative)
        );
        return TwiML(response);
    }
}