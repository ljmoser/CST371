using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CST371.GoogleApi
{
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


}