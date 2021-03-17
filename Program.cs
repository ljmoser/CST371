using System;
using System.Threading.Tasks;
using CST371.GoogleApi;
using CST371.ResultFormatters;
using Microsoft.Extensions.Configuration;

namespace CST371
{
    public class Args
    {
        public string Cuisine {get;set;}
        public string Address {get;set;}
        public string Format {get;set;}
    }

    public static class Program
    {
        private static Args ParseArgs(string[] args)
        {
            string query = args[0];
            return new Args(){
                Cuisine = query.Split("near")[0],
                Address = query.Split("near")[1],
                Format = args[1]
            };
        }

        public static async Task Main(string[] args)
        {
            Args a = ParseArgs(args);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IGoogleApiClient client= new GoogleApiClientComposed(config["apikey"]);
            var placesRes = await client.GetFoodNearAddress(a.Cuisine, a.Address).ConfigureAwait(false);

            IFormatter formatter;
            if(a.Format == "CSV")
                formatter = new CsvFormatter();
            else if (a.Format == "Natural")
                formatter = new NaturalFormatter();
            else if (a.Format == "JSON")
                formatter = new JsonFormatter();
            else
                throw new NotImplementedException($"Format {a.Format} specified but no implementation found");

            string formattedResults = formatter.Format(placesRes);

            Console.WriteLine(formattedResults);
        }
    }
}
