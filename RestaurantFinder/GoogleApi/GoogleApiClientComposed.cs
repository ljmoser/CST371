using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CST371.GoogleApi
{
    public class GoogleApiClientComposed : IGoogleApiClient
    {
        private readonly HttpClient _client = new();
        private readonly string _apiKey;

        public GoogleApiClientComposed(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {
            string geocodingResStr = await _client.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={_apiKey}").ConfigureAwait(false);
            var geocodingRes = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingResStr);

            string placesResStr = await _client.GetStringAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={geocodingRes.results[0].geometry.location.lat}, {geocodingRes.results[0].geometry.location.lng}&radius=1500&type=restaurant&keyword={cuisine}&key={_apiKey}").ConfigureAwait(false);
            var placesRes = JsonConvert.DeserializeObject<PlacesResponse>(placesResStr);

            return placesRes;
        }

        public TimeSpan Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }
    }
}