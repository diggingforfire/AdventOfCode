using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace _12._02
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");

            dynamic parsedJson = JsonConvert.DeserializeObject<ExpandoObject>(text);

            List<object> objects = SelectMany(parsedJson);
            var sum = objects.Sum(obj => (obj as long?) ?? 0);

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        static IEnumerable<object> Flatten(IEnumerable<object> enumerable)
        {
            List<object> objects = new List<object>();

            foreach (var obj in enumerable)
                if (obj is ExpandoObject)
                    objects.AddRange(SelectMany(obj));
                else if (obj is IEnumerable<object>)
                    objects.AddRange(Flatten(obj as IEnumerable<object>));
                else
                    objects.Add(obj);

            return objects;
        }

        static List<object> SelectMany(dynamic expando)
        {
            List<object> objects = new List<object>();

            if (!(expando as IDictionary<string, object>).Any(kvp => kvp.Value.ToString() == "red"))
            {
                foreach (KeyValuePair<string, object> kvp in expando)
                    if (kvp.Value is ExpandoObject)
                        objects.AddRange(SelectMany(kvp.Value));
                    else if (kvp.Value is IEnumerable<object>)
                        objects.AddRange(Flatten(kvp.Value as IEnumerable<object>));
                    else
                        objects.Add(kvp.Value);
            }

            return objects;
        }
    }
}