using CST371.GoogleApi;
using System.Text;

namespace CST371.ResultFormatters
{
    public class NaturalFormatter: IFormatter
    {
        public string Format(PlacesResponse response)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(var res in response.results)
            {
                stringBuilder.AppendLine($"{res.name} has {res.rating} rating and is currently {(res.opening_hours.open_now? "Open":"Closed")}");
            }

            return stringBuilder.ToString();

        } 

    }

}