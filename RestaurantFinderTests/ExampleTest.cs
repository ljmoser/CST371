using System;
using Xunit;
using CST371.GoogleApi;

namespace RestaurantFinderTests
{
    public class ExampleTest
    {
        [Fact]
        public async void Example()
        {
            GoogleApiClientFake fake = new GoogleApiClientFake();
            var response = await fake.GetFoodNearAddress("some string","Some other string");
            Assert.Equal(3, response.results.Length);
        }
    }
}
