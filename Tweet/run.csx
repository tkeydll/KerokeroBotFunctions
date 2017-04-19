#r "Newtonsoft.Json"
#r "System.Configuration"

using System;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using CoreTweet;
using CoreTweet.Core;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    long ret = 0;
    string msg = string.Empty;

    log.Info($"Webhook was triggered at {DateTime.Now}.");

    string jsonContent = await req.Content.ReadAsStringAsync();
    dynamic data = JsonConvert.DeserializeObject(jsonContent);

    log.Verbose($"input: {data}");

    try
    {
        var tweetText = $"{data.body.TweetText}";
        var mediaUrl = $"{data.body.MediaUrl}";
        long replyToStatusId = data.body.ReplyToStatusId;

        if (replyToStatusId != 0)
        {
            var replyTo = ConfigurationManager.AppSettings["REPLY_TO"];
            //tweetText = replyTo + " " + tweetText;
        }

        ret = Tweet(replyToStatusId, tweetText, mediaUrl);
    }
    catch (Exception e)
    {
        log.Info(e.ToString());
        throw;
    }

    return req.CreateResponse(HttpStatusCode.OK, new {
        status_id = ret
    });
}

private static long Tweet(long replyToStatusId, string tweetText, string mediaUrl)
{
    var apiKey = ConfigurationManager.AppSettings["TWITTER_API_KEY"];
    var apiSecret = ConfigurationManager.AppSettings["TWITTER_API_SECRET"];
    var accessToken = ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"];
    var accessSecret = ConfigurationManager.AppSettings["TWITTER_ACCESS_SECRET"];

    var client = CoreTweet.Tokens.Create(apiKey, apiSecret, accessToken, accessSecret);

    var response = client.Statuses.UpdateAsync(status => tweetText, in_reply_to_status_id => replyToStatusId);
    //param["media"] = (new System.Net.WebClient()).OpenRead(mediaUrl);

    return response.Id;
}
