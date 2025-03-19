using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FitnessProjectOOP
{

    public class ExerciseApi
    {
        private static readonly HttpClient client = new HttpClient();

        public string BodyPart { get; set; }

        public async Task FetchExercisesAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://exercisedb.p.rapidapi.com/exercises/bodyPart/{BodyPart}?limit=4&offset=0"),
                Headers =
                {
                    { "x-rapidapi-key", "4f0c197b21mshdf077300ef190aep12b76djsn6d594e9e9bdd" }, 
                    { "x-rapidapi-host", "exercisedb.p.rapidapi.com" },
                },
            };

            try
            {
                // Send request asynchronously
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();  // Throw exception if the status code is not successful

                // Read and output response body
                var body = await response.Content.ReadAsStringAsync();



                Console.WriteLine(body);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
        }
    


    //public static async Task Main(string[] args)
    //    {
    //        var api = new ExerciseApi();
    //        await api.FetchExercisesAsync();
    //    }


    }
}
