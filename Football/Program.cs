using System;
using RestSharp;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Text.Json;



namespace Football
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int year = 2019;
            using (var db = new FootballContext())
            {
                db.Database.EnsureCreated();

                var client = new RestClient("https://api.collegefootballdata.com");
                var teamsRequest = new RestRequest($"/ teamsRequest / fbs ? year ={year}", Method.GET);

                var response = await client.ExecuteAsync(teamsRequest);
                var content = response.Content;
                var teams = JsonSerializer.Deserialize<Team[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var tasks = new List<Task<IRestResponse>>();
                foreach (var team in teams)
                {
                    var coachRequest = new RestRequest($"/coaches?team={team.School}&year={year}", Method.GET);
                    tasks.Add(client.ExecuteAsync(coachRequest));

                }
                var responses = await Task.WhenAll(tasks);
                var coaches = new responses.SelectMany(x => JsonSerializer.Deserialize<Coach[]>(x.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

                foreach (var coach in coaches)
                {
                    teams.Single(x => x.School == coach.Seasons.First().School)
                        .Coaches.Add(coach);

                }

                var addTasks = teams.Select(x => db.AddAsync(x).AsTask());
                await Task.WhenAll(addTasks);
                await db.SaveChangesAsync();

            }

                





        }
    }
}
