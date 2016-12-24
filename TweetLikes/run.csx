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

    log.Info($"Webhook was triggered!");


    string jsonContent = await req.Content.ReadAsStringAsync();
    dynamic data = JsonConvert.DeserializeObject(jsonContent);

    log.Verbose($"{data}");

    long id = data.body.TweetId;

    log.Info("status_id = " + id.ToString());

    ret = Like(id);

    return req.CreateResponse(HttpStatusCode.OK, new {
        status_id = ret
    });
}

private static long Like(long statusId)
{
    var apiKey = ConfigurationManager.AppSettings["TWITTER_API_KEY"];
    var apiSecret = ConfigurationManager.AppSettings["TWITTER_API_SECRET"];
    var accessToken = ConfigurationManager.AppSettings["TWITTER_ACCESS_TOKEN"];
    var accessSecret = ConfigurationManager.AppSettings["TWITTER_ACCESS_SECRET"];

    var client = CoreTweet.Tokens.Create(apiKey, apiSecret, accessToken, accessSecret);

    var response = client.Favorites.CreateAsync(statusId);
    return response.Id;
}
