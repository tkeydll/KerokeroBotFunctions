#r "System.Configuration"

using System;
using System.Configuration;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    string tweetText = string.Empty;

    var d = DateTime.Now;

    switch (d.Hour)
    {
        case 14:
            log.Info($"23時");
            if (d.Minute == 59)
            {
                tweetText = "良いお年を！";
            }
            break;
        case 15:
            log.Info($"0時");
            if (d.Minute == 0)
            {
                tweetText = "明けましておめでとうけろ🐸";
            }
            break;
        default:
            return;
    }

    var tweetId = Tweet(null, tweetText, null);

    log.Info($"TweetId: {tweetId}");
}

public static long Tweet(long? replyToStatusId, string tweetText, string mediaUrl)
{
    var apiKey = ConfigurationManager.AppSettings["TWITTER_API_KEY"];
    var apiSecret = ConfigurationManager.AppSettings["TWITTER_API_SECRET"];
    var accessToken = ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"];
    var accessSecret = ConfigurationManager.AppSettings["TWITTER_ACCESS_SECRET"];

    var client = CoreTweet.Tokens.Create(apiKey, apiSecret, accessToken, accessSecret);

    var param = new Dictionary<string, object>();
    param["status"] = tweetText;

    var response = client.Statuses.Update(param);

    return response.Id;
}
