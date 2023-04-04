using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockAppPractice.Models;
using StockAppPractice.ServiceContracts;

namespace StockAppPractice.Controllers
{
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;

        public TradeController(IOptions<TradingOptions> tradingOptions,
                               IFinnhubService finnhubService)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultSymbolToken is null)
            {
                _tradingOptions.DefaultSymbolToken = "MSFT";
            }

            string? symbolToken = _tradingOptions.DefaultSymbolToken;

            var companyProfiles = await _finnhubService.GetCompanyProfile(symbolToken);
            var quotes = await _finnhubService.GetStockPriceQuote(symbolToken);

            StockTrade stockTrade = new()
            {
                Price = Convert.ToDouble(quotes["c"].ToString()),
                StockSymbol = symbolToken,  
                StockName = Convert.ToString(companyProfiles["name"].ToString())
            };

            return View(stockTrade);
        }
    }
}
