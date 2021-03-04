using System;
using System.Threading.Tasks;
using CST371.GoogleApi;
using CST371.ResultFormatters;

namespace CST371
{
    public class Args
    {
        public string Cuisine {get;set;}
        public string Address {get;set;}
        public string Format {get;set;}
    }

    class Program
    {

        static Args ParseArgs(string[] args)
        {
            string query = args[0];
            return new Args(){
                Cuisine = query.Split("near")[0],
                Address = query.Split("near")[1],
                Format = args[1]
            };
        }

        static async Task Main(string[] args)
        {
            Args a = ParseArgs(args);

            IGoogleApiClient client= new GoogleApiClientFake();
            var placesRes = await client.GetFoodNearAddress(a.Cuisine, a.Address);
            
            IFormatter formatter = new NaturalFormatter();
            string formattedResults = formatter.Format(placesRes);

            Console.WriteLine(formattedResults);
        }
    }
}
