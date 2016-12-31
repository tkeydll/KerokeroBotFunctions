#r "System.Configuration"

using System;
using System.Configuration;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    string tweetText = string.Empty;

    var d = DateTime.Now;

    log.Info(d.Hour);
    log.Info(d.Minute);

    switch (d.Hour)
    {
        case 14:
            if (d.Minute == 56)
            {
                tweetText = "今年一年世話になったけろ🐸";
            }
            if (d.Minute == 57)
            {
                tweetText = "来年もよろしく。";
            }
            if (d.Minute == 58)
            {
                tweetText = "良いお年を！";
            }
            break;
        case 15:
            if (d.Minute == 0)
            {
                tweetText = "明けましておめでとうけろ🐸🐸🐸";
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
