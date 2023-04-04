namespace StockAppPractice.Models
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; }

        public string? Unit { get; set; }
    }
}
