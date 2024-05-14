using FinanceBot.Models;

namespace FinanceBot.ViewModels
{
    public class CryptoViewModel
    {
        public List<CryptoCurrency> RisingCryptos { get; set; }
        public List<CryptoCurrency> FallingCryptos { get; set; }
        public List<CryptoCurrency> CheapestCryptos { get; set; }
        public List<CryptoCurrency> MostExpensiveCryptos { get; set; }
        public List<Stock> RisingStocks { get; set; }
        public List<Stock> FallingStocks { get; set; }
        public List<Stock> CheapestStocks { get; set; }
        public List<Stock> MostExpensiveStocks { get; set; }
    }
}
