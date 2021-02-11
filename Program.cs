using System.Dynamic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CST371.GoogleApi;


namespace CST371
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string input = args[0];

            HttpClient client = new HttpClient();
            string cuisine = input.Split("near")[0];
            string address = input.Split("near")[1];
            string geocodingResStr = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=1234");
            var geocodingRes = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingResStr);

            string placesResStr = await client.GetStringAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={geocodingRes.results[0].geometry.location.lat}, {geocodingRes.results[0].geometry.location.lng}&radius=1500&type=restaurant&keyword={cuisine}&key=<yourApiKey>");
            var placesRes = JsonConvert.DeserializeObject<PlacesResponse>(placesResStr);

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
