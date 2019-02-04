# Dynamic JSON Properties
This project is about dealing with dynamic json data using c#, spoiler alert it the Dictionary object for the win.

If you're building and cosuming your own API using Visual Studio and the Web API then you pprobably won't need this information, If, however, you're consuming an API which was written in another language then you will probably run accross this issue.

Mostly when I’ve seen it, it looks like an attempt to save a few bytes of data.
Here is the JSON I needed to (de)serialize:

{
     "object": "choo",
     "data": {
           "SSL-Test": {
                "SSL Security": {
                     "enable-sni": "yes",
                     "domain": ["test.com"],
                     "sni-certificate": ["test-cert"]
                },
                "name": "SSL-Test"
           }
     },
     "token": "cha"
}

The highlighted field name is dynamic, it’s the name of the “service” in the barracuda. In other words, I won’t know until the property name until runtime.
This is a problem because while I could hard code the name it means I would be constantly changing my code to accommodate new services.
Fortunately JSON.net has a way to deal with this using the Dictionary object in the form of Dictionary<string, Class>. 
The tricky part is figuring out how the classes line up, so here are the rules:

The property that contains the dynamic portion of JSON data becomes the Dictionary<string, Class>.
The body of that property becomes its own class.

So in the case above it looks something like this:

public class SniResponse
{
    [JsonProperty("object")]
    public string ServiceObject { get; set; }

    [JsonProperty("data")]
    public Dictionary<string, Service> Data { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}


public class Service
{
    [JsonProperty("SSL Security")]
    public SSL_Security Security { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}

public class SSL_Security
{
    [JsonProperty("enable-sni")]
    public string EnableSni { get; set; }

    [JsonProperty("domain")]
    public List<string> Domains { get; set; }

    [JsonProperty("sni-certificate")]
    public List<string> SniCertificates { get; set; }
}


