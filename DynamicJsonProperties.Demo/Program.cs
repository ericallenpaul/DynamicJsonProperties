using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DynamicJsonProperties.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //get some JSON data
            string json = System.IO.File.ReadAllText("dynamic.json");

            //deserialize the JSON and create a class
            Colors colors = JsonConvert.DeserializeObject<Colors>(json);

            //let's write the color list to screen
            foreach (var color in colors.ColorsRoot.Keys)
            {
                //write the color to the screen
                Console.WriteLine($"Color: {color}");
                Console.WriteLine("----------------------");

                //what about the child properties?
                //print the category
                Console.WriteLine($"\tCategory: {colors.ColorsRoot[color].Category}");

                //print the Hex code
                Console.WriteLine($"\tHex Code: {colors.ColorsRoot[color].Code.Hex}");

                //convert the rgba array to a csv string
                var stringResult = string.Join(",", colors.ColorsRoot[color].Code.Rgba);
                //print the RGB string
                Console.WriteLine($"\tRGB Code: {stringResult}");

                //print the color type
                Console.WriteLine($"\tType: {colors.ColorsRoot[color].Type}");

                //blank line
                Console.WriteLine(System.Environment.NewLine);
            }

            //leave the console paused
            Console.Read();

        }
    }
}
