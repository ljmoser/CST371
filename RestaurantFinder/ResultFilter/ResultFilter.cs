using CST371.GoogleApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CST371.ResultFilter
{
    public class ResultFilter
    {
        public PlacesResponse FilterResults(PlacesResponse originalResults, bool? isOpen, string? substringOfName, double? minimumRating)
        {
            var resultsList = originalResults.results.ToList();

            if(isOpen.HasValue)
            {
                var list = new List<PlacesResult>();
                foreach(var result in resultsList)
                {
                    if(result.opening_hours.open_now == isOpen)
                    {
                        list.Add(result);
                    }
                }
                resultsList = list;
            }

            if(substringOfName != null)
            {
                resultsList = resultsList.Where(x=>x.name.Contains(substringOfName)).ToList();
            }

            if(minimumRating.HasValue)
            {
                resultsList = resultsList.Where(x=>x.rating >= minimumRating).ToList();
            }

            originalResults.results = resultsList.ToArray();

            // make your changes to originalResultsHere
            return originalResults;
        }
    }
}