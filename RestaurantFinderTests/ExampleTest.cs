using System;
using Xunit;
using CST371.GoogleApi;
using CST371.ResultFormatters;
using CST371;
using System.Linq;
using CST371.ResultFilter;

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

        [Fact]
        public async void GetFormatter_CSV()
        {
            var formatter = Program.GetFormatter(new Args{Format="CSV"}, "");
            Assert.True(formatter is CsvFormatter);
        }

        [Fact]
        public async void GetFormatter_csv()
        {
            var formatter = Program.GetFormatter(new Args{Format="csv"}, "");
            Assert.True(formatter is CsvFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultCSV()
        {
            var formatter = Program.GetFormatter(new Args(), "CSV");
            Assert.True(formatter is CsvFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultcsv()
        {
            var formatter = Program.GetFormatter(new Args(), "csv");
            Assert.True(formatter is CsvFormatter);
        }

        [Fact]
        public async void GetFormatter_Natural()
        {
            var formatter = Program.GetFormatter(new Args{Format="Natural"}, "");
            Assert.True(formatter is NaturalFormatter);
        }

        [Fact]
        public async void GetFormatter_natural()
        {
            var formatter = Program.GetFormatter(new Args{Format="natural"}, "");
            Assert.True(formatter is NaturalFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultNatural()
        {
            var formatter = Program.GetFormatter(new Args(), "Natural");
            Assert.True(formatter is NaturalFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultnatural()
        {
            var formatter = Program.GetFormatter(new Args(), "natural");
            Assert.True(formatter is NaturalFormatter);
        }

        [Fact]
        public async void GetFormatter_JSON()
        {
            var formatter = Program.GetFormatter(new Args{Format="JSON"}, "");
            Assert.True(formatter is JsonFormatter);
        }

        [Fact]
        public async void GetFormatter_json()
        {
            var formatter = Program.GetFormatter(new Args{Format="json"}, "");
            Assert.True(formatter is JsonFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultJSON()
        {
            var formatter = Program.GetFormatter(new Args(), "JSON");
            Assert.True(formatter is JsonFormatter);
        }

        [Fact]
        public async void GetFormatter_defaultjson()
        {
            var formatter = Program.GetFormatter(new Args(), "json");
            Assert.True(formatter is JsonFormatter);
        }

        [Fact]
        public void FormatCommaWithCSV()
        {
            var formatter = new CsvFormatter();
            var response = new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Name, with, commas", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, vicinity = "near by"},
                }
            };
            string formattedResult = formatter.Format(response);
            var lines = formattedResult.Split(Environment.NewLine);
            Assert.Equal(2, lines[1].Count(x => x == ','));
        }

        [Fact]
        public void Filter_IsOpen_Null()
        {
            var filterer = new ResultFilter();
            var response = new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Name A", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, vicinity = "near by"},
                    new PlacesResult(){name = "Name B", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, vicinity = "near by"},
                }
            };
            var filteredResults = filterer.FilterResults(response, null, null, null);
            Assert.Equal(2, filteredResults.results.Length);
        }

        [Fact]
        public void Filter_IsOpen_True()
        {
            var filterer = new ResultFilter();
            var response = new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Name A", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, vicinity = "near by"},
                    new PlacesResult(){name = "Name B", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, vicinity = "near by"},
                }
            };
            var filteredResults = filterer.FilterResults(response, true, null, null);
            Assert.Equal(1, filteredResults.results.Length);
            Assert.Equal("Name B", filteredResults.results[0].name);
        }

        [Fact]
        public void Filter_IsOpen_False()
        {
            var filterer = new ResultFilter();
            var response = new PlacesResponse()
            {
                results = new PlacesResult[]{
                    new PlacesResult(){name = "Name A", opening_hours = new OpeningHours{open_now = false}, rating = 4.8, vicinity = "near by"},
                    new PlacesResult(){name = "Name B", opening_hours = new OpeningHours{open_now = true}, rating = 4.8, vicinity = "near by"},
                }
            };
            var filteredResults = filterer.FilterResults(response, false, null, null);
            Assert.Equal(1, filteredResults.results.Length);
            Assert.Equal("Name A", filteredResults.results[0].name);
        }
    }

}
