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
        public static Args ParseArgs(string[] args)
        {
            string query = args[0];
            string format = args.Length == 1 ? null : args[1];
            return new Args(){
                Cuisine = query.Split("near")[0],
                Address = query.Split("near")[1],
                Format = format
            };
        }

        public static IFormatter GetFormatter(Args a, string defaultFormat)
        {
            string format = a.Format??defaultFormat;

            if (format == "CSV")
                return new CsvFormatter();
            else if (format == "Natural")
                return new NaturalFormatter();
            else if (format == "JSON")
                return new JsonFormatter();
            else
                throw new NotImplementedException($"Format {a.Format} specified but no implementation found");
        }

        public static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Args a = ParseArgs(args);

            IGoogleApiClient client= new GoogleApiClientComposed(config["apikey"]);
            var placesRes = await client.GetFoodNearAddress(a.Cuisine, a.Address).ConfigureAwait(false);

            var formatter = GetFormatter(a, config["defaultFormat"]);
            string formattedResults = formatter.Format(placesRes);

            Console.WriteLine(formattedResults);
        }
    }
}
