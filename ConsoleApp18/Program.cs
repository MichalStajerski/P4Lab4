using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            var google = new RestClient("http://google.pl");
            //var request = new RestRequest("/"); http://google.pl/
            var ath = new RestClient("http://ath.bielsko.pl");
            var wiki = new RestClient("http://pl.wikipedia.org");
            var zus = new RestClient("http://www.zus.pl");
            var gov = new RestClient("http://gov.pl");




            var tasks = new List<Task>();


            //get-pobrac zasob
            //post-zapisac
            //execute nie narzuca metody
           stopwatch.Start();
            //var response =  client.Execute(request, Method.GET);

            //executeAsync nie czeka na odpowiedz dlatego mozna uzyc await
            /*
            await google.ExecuteAsync(new RestRequest("/"), Method.GET);
            Console.WriteLine(stopwatch.Elapsed);
            
            await ath.ExecuteAsync(new RestRequest("/wiki/Dyskografia_Soundggarden"), Method.GET);
            Console.WriteLine(stopwatch.Elapsed);

            await gov.ExecuteAsync(new RestRequest("/"), Method.GET);
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine(stopwatch.Elapsed);
            */


            tasks.Add(google.ExecuteAsync(new RestRequest("/"), Method.GET));
            Console.WriteLine(stopwatch.Elapsed);

            tasks.Add(ath.ExecuteAsync(new RestRequest("/wiki/Dyskografia_Soundggarden"), Method.GET));
            Console.WriteLine(stopwatch.Elapsed);

            tasks.Add(gov.ExecuteAsync(new RestRequest("/"), Method.GET));
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("----------------------");
            await Task.WhenAny(tasks);
            Console.WriteLine(stopwatch.Elapsed);
            await Task.WhenAll(tasks);
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Stop();


            var response = await Website.Download("https://api.collegefootballdata.com", "/play/stats?year=2019");
            var json = response.Content;
            var result = JsonConvert.DeserializeObject<Play[]>(json);

        }
    }
}
