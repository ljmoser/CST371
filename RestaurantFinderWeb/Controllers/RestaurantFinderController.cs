using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST371.GoogleApi;
using CST371.ResultFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RestaurantFinderWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantFinderController : ControllerBase
    {
        private readonly ILogger<RestaurantFinderController> _logger;
        private readonly IConfiguration _config;

        public RestaurantFinderController(ILogger<RestaurantFinderController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet("/restaurants/")]
        public async Task<PlacesResponse> Get([FromQuery] string address, [FromQuery] string cuisine, [FromQuery] string name, [FromQuery] double? rating, [FromQuery] bool? isOpen)
        {
            IGoogleApiClient client= new GoogleApiClientFake();
            var resultsPriorToFilter = await client.GetFoodNearAddress(cuisine, address);
            var filter = new ResultFilter();
            var resultsAfterFitler = filter.FilterResults(resultsPriorToFilter, isOpen, name, rating);
            return resultsAfterFitler;
            
        }
    }
}
