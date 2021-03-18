using System.Threading.Tasks;

namespace CST371.GoogleApi
{
    public interface IGoogleApiClient
    {
        Task<PlacesResponse> GetFoodNearAddress(string cuisine, string address);
    }
}