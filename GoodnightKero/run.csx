#r "System.Configuration"

using System;
using System.Configuration;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var messages = InitMessage();
    var tweetText = GetRandomMessage(messages);
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

private static string GetRandomMessage(IList<string> data)
{
    var r = new Random(Environment.TickCount);
    var i = r.Next(data.Count);
    return data[i];
}

private static IList<string> InitMessage()
{
    var list = new List<string>();
    list.Add("Zzzzzz🐸");
    list.Add("おやすみけろ🐸✨");
    list.Add("おやすみけろ🐸💤");
    list.Add("ねる！");
    list.Add("永眠しそう。");
    return list;
}

