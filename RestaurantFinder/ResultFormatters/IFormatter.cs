namespace CST371.ResultFormatters
{
    public interface IFormatter
    {
        string Format(GoogleApi.PlacesResponse response);
    }
}