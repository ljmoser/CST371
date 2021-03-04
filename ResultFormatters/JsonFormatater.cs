using CST371.GoogleApi;
using System.Text;

namespace CST371.ResultFormatters
{
    public class JsonFormatter: IFormatter
    {
        public string Format(PlacesResponse response)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(response.results);
        } 
    }
}