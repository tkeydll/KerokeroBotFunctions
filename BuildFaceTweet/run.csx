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
    log.Info($"Webhook was triggered!");

    string jsonContent = await req.Content.ReadAsStringAsync();
    dynamic data = JsonConvert.DeserializeObject(jsonContent);

    //log.Verbose($"{data}");

    var faceAttributes = data[0].faceAttributes;

    return req.CreateResponse(HttpStatusCode.OK, new {
        TweetText = $"{faceAttributes.age}"
    });
}
