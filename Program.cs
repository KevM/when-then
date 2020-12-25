using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp3
{
    public static class MSSystemExtenstions
    {
        private static Random rng = new Random();

        public static IEnumerable<T> Randomize<T>(IEnumerable<T> input)
        {
            var inputList = input.ToList();
            List<T> output = new List<T>();
            int i = 0;

            while (inputList.Count > 0)
            {
                int index = rng.Next(inputList.Count);
                output[i++] = inputList[index];
                inputList.RemoveAt(index);
            }

            return output;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            var source = list.ToList();
            int n = source.Count;
            var shuffled = new List<T>(n);
            shuffled.AddRange(source);
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = shuffled[k];
                shuffled[k] = shuffled[n];
                shuffled[n] = value;
            }
            return shuffled;
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
