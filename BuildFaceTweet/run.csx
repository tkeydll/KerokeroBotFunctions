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

    var faceAttributes = data.body.faceAttributes;

    log.Verbose($"{data}");

    return req.CreateResponse(HttpStatusCode.OK, new {
        age = faceAttributes.age
    });
}
