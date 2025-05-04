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

        // Calls the API for the specific body part, it returns a list of exercises 

        public async Task<List<Exercise>> GetExercisesByBodyPartAsync(string bodyPart)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://exercisedb.p.rapidapi.com/exercises/bodyPart/{bodyPart.ToLower()}?limit=10"),
                Headers =
            {
                { "x-rapidapi-key", "4f0c197b21mshdf077300ef190aep12b76djsn6d594e9e9bdd" },
                { "x-rapidapi-host", "exercisedb.p.rapidapi.com" },
            },
            };

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Exercise>>(body);
            }
            catch
            {
                return new List<Exercise>(); // Return empty list on error
            }
        }
    }



    //public static async Task Main(string[] args)
    //    {
    //        var api = new ExerciseApi();
    //        await api.FetchExercisesAsync();
    //    }


}

