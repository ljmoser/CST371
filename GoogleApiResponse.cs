namespace CST371.GoogleApi
{
    public class GeocodingResponse
    {
        public GeocodingResult[] results { get; set; }
        public string status { get; set; }
    }

    public class GeocodingResult
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class PlacesResponse
    {
        public PlacesResult[] results { get; set; }
        public string status { get; set; }
    }

    public class PlacesResult
    {
        public OpeningHours opening_hours { get; set; }
        public string name {get;set;}

        public double rating {get;set;}

        public int user_ratings_total {get;set;}
        
        public string vicinity {get;set;}

    }

    public class OpeningHours
    {
        public bool open_now {get;set;}

    }
}