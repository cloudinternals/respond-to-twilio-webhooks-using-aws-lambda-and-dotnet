using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;

namespace TwilioWebhookLambda.MinimalLambdaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordingStatusChangeController : TwilioController
{
    [HttpPost]
    public async Task Index()
    {
        string recordingUrl = Request.Form["RecordingUrl"];
        string fileName = $"{recordingUrl.Substring(recordingUrl.LastIndexOf("/") + 1)}.mp3";
        string bucketName = "my-twilio-call-recordings";

        using HttpClient client = new HttpClient(); // use HttpClient factory in production
        using HttpResponseMessage response = await client.GetAsync($"{recordingUrl}.mp3");
        using Stream recordingFileStream = await response.Content.ReadAsStreamAsync();
        using var s3Client = new AmazonS3Client();
        using var transferUtility = new TransferUtility(s3Client);
        await transferUtility.UploadAsync(recordingFileStream, bucketName, fileName);
    }
}