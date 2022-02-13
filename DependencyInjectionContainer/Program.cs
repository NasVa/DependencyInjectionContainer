using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            // When a program often has to try keys that turn out not to
            // be in the dictionary, TryGetValue can be a more efficient
            // way to retrieve values.
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("a", "b");
            string value = "";
            if (dictionary.TryGetValue("a", out value))
            {
                Console.WriteLine("For key = \"tif\", value = {0}.", value);
            }
            else
            {
                Console.WriteLine("Key = \"tif\" is not found.");
            }
        }
    }
}
