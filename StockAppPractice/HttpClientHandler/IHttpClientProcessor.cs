namespace StockAppPractice.HttpClientHandler
{
    public interface IHttpClientProcessor
    {
        Task<Dictionary<string, object>?> GetJsonDataReadAsStream(string uri);
    }
}
