using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TwilioWebhookSample.MinimalLambdaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IncomingCallController : TwilioController
{
    [HttpPost]
    public TwiMLResult Index()
    {
        var response = new VoiceResponse();
        response.Say("Hello. Please leave a message after the beep.");
        response.Record(
            timeout: 10,
            recordingStatusCallback: new Uri("https://oypxy6bjdu77k7setj5kujpmxy0daiey.lambda-url.us-east-2.on.aws/RecordingStatusChange")
        );
        return TwiML(response);    
    }
}