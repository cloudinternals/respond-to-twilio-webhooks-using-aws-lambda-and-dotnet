using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;

namespace TwilioWebhookSample.MinimalLambdaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecordingStatusChangeController : TwilioController
{
    [HttpPost]
    public async Task Index()
    {
        string recordingUrl = Request.Form["RecordingUrl"];
        string fileName = $"{recordingUrl.Substring(recordingUrl.LastIndexOf("/") + 1)}.mp3";
        string bucketName = "my-twilio-call-recordings";

        using HttpClient client = new HttpClient();
        using HttpResponseMessage response = await client.GetAsync(recordingUrl);
        using Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
        using var s3Client = new AmazonS3Client();
        using var transferUtility = new TransferUtility(s3Client);
        await transferUtility.UploadAsync(streamToReadFrom, bucketName, fileName);
    }
}