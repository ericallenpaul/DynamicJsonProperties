# Dynamic JSON Properties
This project is about dealing with dynamic json data using c#, spoiler alert it's 
the Dictionary object for the win.

If you're building and cosuming your own API using Visual Studio and the Web API then you 
probably won't need this information, If, however, you're consuming an API which was 
written in another language then you will probably run accross this issue.

Mostly when I’ve seen it, it looks like an attempt to save a few bytes of data.
Here is an example of a normal bit of JSON data:

```json
{
  "colors": [
    {
      "color": "black",
      "category": "hue",
      "type": "primary",
      "code": {
        "rgba": [255,255,255,1],
        "hex": "#000"
      }
    },
    {
      "color": "white",
      "category": "value",
      "code": {
        "rgba": [ 0, 0, 0, 1 ],
        "hex": "#FFF"
      }
    },
    {
      "color": "red",
      "category": "hue",
      "type": "primary",
      "code": {
        "rgba": [255,0,0,1],
        "hex": "#FF0"
      }
    },
  ]
}
```

Every property has a name and a value. It would translate to a c# class that would look like this:

```csharp
public partial class Colors
{
    [JsonProperty("colors")]
    public Color[] ColorsRoot { get; set; }
}

public partial class Color
{
    [JsonProperty("color")]
    public string ColorColor { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    [JsonProperty("code")]
    public Code Code { get; set; }
}

public partial class Code
{
    [JsonProperty("rgba")]
    public long[] Rgba { get; set; }

    [JsonProperty("hex")]
    public string Hex { get; set; }
}
```

In truth, the `"color": "red",` wastes a few bytes because `"color:"` is actually unecessary.
If every byte matters this can easily be re-written like this:

```JSON
{
  "colors": 
  {
    "black": {
      "category": "hue",
      "type": "primary",
      "code": {
        "rgba": [ 255, 255, 255, 1 ],
        "hex": "#000"
      }
    },

    "white": {
      "category": "value",
      "code": {
        "rgba": [ 0, 0, 0, 1 ],
        "hex": "#FFF"
      }
    },

    "red": {
      "category": "hue",
      "type": "primary",
      "code": {
        "rgba": [ 255, 0, 0, 1 ],
        "hex": "#FF0"
      }
    }
  }
}
```

This presents a problem however if you're used to the more normal JSON format. You no longer
Have a static class name for color, just the color itself. The field name for color is dynamic, 
it’s the name of the “color”. In other words, we won’t know the property name until runtime.
This is a problem because while I could hard code the color it means I would be constantly 
changing my code to accommodate new colors.
Fortunately JSON.net has a way to deal with this using the Dictionary object in the form 
of Dictionary<string, Class>. 
The tricky part is figuring out how the classes line up, so here are the rules:

The property that contains the dynamic portion of JSON data becomes the Dictionary<string, Class>.
The body of that property becomes its own class.

So in the case above it looks something like this:


