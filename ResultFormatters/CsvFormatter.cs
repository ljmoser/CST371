using CST371.GoogleApi;
using System.Text;

namespace CST371.ResultFormatters
{
    public class CsvFormatter: IFormatter
    {
        public string Format(PlacesResponse response)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"name, rating, is_open_now");

            foreach(var res in response.results)
            {
                stringBuilder.AppendLine($"{res.name}, {res.rating}, {(res.opening_hours.open_now? "Open":"Closed")}");
            }

            return stringBuilder.ToString();

        } 

    }

}