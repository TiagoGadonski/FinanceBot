using FinanceBot.Models;

namespace FinanceBot.Services
{
    public class AnalysisService
    {
        public List<CryptoCurrency> GetRisingCryptos(List<CryptoCurrency> cryptos)
        {
            return cryptos.Where(c => c.PriceChangePercentage24h > 0).OrderByDescending(c => c.PriceChangePercentage24h).ToList();
        }

        public List<CryptoCurrency> GetFallingCryptos(List<CryptoCurrency> cryptos)
        {
            return cryptos.Where(c => c.PriceChangePercentage24h < 0).OrderBy(c => c.PriceChangePercentage24h).ToList();
        }

        public List<CryptoCurrency> GetCheapestCryptos(List<CryptoCurrency> cryptos)
        {
            return cryptos.OrderBy(c => c.CurrentPrice).Take(10).ToList();
        }

        public List<CryptoCurrency> GetMostExpensiveCryptos(List<CryptoCurrency> cryptos)
        {
            return cryptos.OrderByDescending(c => c.CurrentPrice).Take(10).ToList();
        }

        public List<Stock> GetRisingStocks(List<Stock> stocks)
        {
            return stocks.Where(s => s.PriceChangePercentage24h > 0).OrderByDescending(s => s.PriceChangePercentage24h).ToList();
        }

        public List<Stock> GetFallingStocks(List<Stock> stocks)
        {
            return stocks.Where(s => s.PriceChangePercentage24h < 0).OrderBy(s => s.PriceChangePercentage24h).ToList();
        }

        public List<Stock> GetCheapestStocks(List<Stock> stocks)
        {
            return stocks.OrderBy(s => s.CurrentPrice).Take(10).ToList();
        }

        public List<Stock> GetMostExpensiveStocks(List<Stock> stocks)
        {
            return stocks.OrderByDescending(s => s.CurrentPrice).Take(10).ToList();
        }

    }
}
