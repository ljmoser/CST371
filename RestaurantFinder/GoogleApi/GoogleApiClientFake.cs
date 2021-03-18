using System.Threading.Tasks;

namespace CST371.GoogleApi
{
        public class GoogleApiClientFake : IGoogleApiClient
    {
        public async Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address)
        {
            return new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Johnny's Sandwich Shop", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, vicinity = "near by"},
                    new PlacesResult(){name = "Amy's Sandwich Shop", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, vicinity = "near by"},
                    new PlacesResult(){name = "Beth's Sandwich Shop", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, vicinity = "near by"},
                }
            };
        }
    }
}