using System;
using System.Threading.Tasks;
using CST371.GoogleApi;


namespace CST371
{

    class Program
    {
        static async Task Main(string[] args)
        {
            string input = args[0];

            string cuisine = input.Split("near")[0];
            string address = input.Split("near")[1];

            IGoogleApiClient client= new GoogleApiClientFake();
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
