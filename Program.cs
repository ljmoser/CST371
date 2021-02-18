using System.Dynamic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CST371.GoogleApi;


namespace CST371
{
    public interface IGoogleApiClient
    {
        Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address);
    }

    public class GoogleApiClientInherited : HttpClient, IGoogleApiClient
    {
        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {

            string geocodingResStr = await GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=<apiKey>");
            var geocodingRes = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingResStr);

            string placesResStr = await GetStringAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={geocodingRes.results[0].geometry.location.lat}, {geocodingRes.results[0].geometry.location.lng}&radius=1500&type=restaurant&keyword={cuisine}&key=<apiKey>");
            var placesRes = JsonConvert.DeserializeObject<PlacesResponse>(placesResStr);

            return placesRes;
        }
    }

    public class GoogleApiClientComposed : IGoogleApiClient
    {
        HttpClient client = new HttpClient();

        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {
            string geocodingResStr = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=<apiKey>");
            var geocodingRes = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingResStr);

            string placesResStr = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={geocodingRes.results[0].geometry.location.lat}, {geocodingRes.results[0].geometry.location.lng}&radius=1500&type=restaurant&keyword={cuisine}&key=<apiKey>");
            var placesRes = JsonConvert.DeserializeObject<PlacesResponse>(placesResStr);

            return placesRes;
        }

        public TimeSpan Timeout
        {
            get{return client.Timeout;}
            set{client.Timeout = value;}
        }
    }

    public class GoogleApiClientFake : IGoogleApiClient
    {
        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {

            return new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Johnny's Sandwich Shop", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, user_ratings_total = 3, vicinity = "near by"},
                    new PlacesResult(){name = "Amy's Sandwich Shop", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, user_ratings_total = 3, vicinity = "near by"},
                    new PlacesResult(){name = "Beth's Sandwich Shop", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, user_ratings_total = 3, vicinity = "near by"},
                }
            };
        }

    }


    class Program
    {
        static async Task Main(string[] args)
        {
            string input = args[0];

            string cuisine = input.Split("near")[0];
            string address = input.Split("near")[1];

            GoogleApiClientComposed client = new GoogleApiClientComposed();
            var placesRes = await client.GetFoodNearAddress(cuisine, address);
            
            foreach (var place in placesRes.results)
            {
                if (place.opening_hours != null && place.opening_hours.open_now)
                {
                    Console.WriteLine(place.name);
                }
            }
        }
    }
}
