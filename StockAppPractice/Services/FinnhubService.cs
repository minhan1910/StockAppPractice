using StockAppPractice.Constants;
using StockAppPractice.HttpClientHandler;
using StockAppPractice.ServiceContracts;

namespace StockAppPractice.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientProcessor _httpClientProcessor; 
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientProcessor httpClientProcessor, 
                              IConfiguration configuration)
        {
            _httpClientProcessor = httpClientProcessor;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string uri = BuildUri(SystemConstant.COMPANY_PROFILE_DOMAIN, stockSymbol);

            return await _httpClientProcessor.GetJsonDataReadAsStream(uri);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string uri = BuildUri(SystemConstant.QUOTE_DOMAIN, stockSymbol);

            return await _httpClientProcessor.GetJsonDataReadAsStream(uri);
        }

        private string BuildUri(string domain, string stockSymbol)
        {
            return $"https://finnhub.io/api/v1/{domain}?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}";
        }
    }
}
