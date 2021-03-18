using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CST371.GoogleApi
{
    public class GoogleApiClientInherited : HttpClient, IGoogleApiClient
    {
        private readonly string apiKey;

        public GoogleApiClientInherited(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {
            string geocodingResStr = await GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}").ConfigureAwait(false);
            var geocodingRes = JsonConvert.DeserializeObject<GeocodingResponse>(geocodingResStr);

            string placesResStr = await GetStringAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={geocodingRes.results[0].geometry.location.lat}, {geocodingRes.results[0].geometry.location.lng}&radius=1500&type=restaurant&keyword={cuisine}&key={apiKey}").ConfigureAwait(false);
            var placesRes = JsonConvert.DeserializeObject<PlacesResponse>(placesResStr);

            return placesRes;
        }
    }
}
