using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockAppPractice.Models;
using StockAppPractice.ServiceContracts;

namespace StockAppPractice.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions,
                               IFinnhubService finnhubService,
                               IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _configuration = configuration;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_tradingOptions.DedaultStockSymbol))
            {
                _tradingOptions.DedaultStockSymbol = "MSFT";
            }

            string? symbolToken = _tradingOptions.DedaultStockSymbol;

            var companyProfiles = await _finnhubService.GetCompanyProfile(symbolToken);

            var quotes = await _finnhubService.GetStockPriceQuote(symbolToken);

            StockTrade stockTrade = new()
            {               
                StockSymbol = symbolToken
            };

            if (companyProfiles is not null && quotes is not null)
            {
                stockTrade.StockName = Convert.ToString(companyProfiles["name"]);
                stockTrade.Price = Convert.ToDouble(quotes["c"].ToString());    
            }

            ViewBag.FinnhubToken = _configuration["FinnhubToken"]; 

            return View(stockTrade);
        }
    }
}
