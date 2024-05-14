using FinanceBot.Services;
using FinanceBot.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinanceBot.Controllers
{
    public class HomeController : Controller
    {
        private readonly CryptoService _cryptoService;
        private readonly StockService _stockService;
        private readonly AnalysisService _analysisService;
        private readonly MLService _mlService;

        public HomeController(CryptoService cryptoService, StockService stockService, AnalysisService analysisService, MLService mlService)
        {
            _cryptoService = cryptoService;
            _stockService = stockService;
            _analysisService = analysisService;
            _mlService = mlService;
        }

        public async Task<IActionResult> Index()
        {
            var cryptos = await _cryptoService.GetCryptoCurrenciesAsync();
            var stocks = await _stockService.GetStocksAsync();

            _mlService.TrainModel(cryptos, stocks);

            var risingCryptos = _analysisService.GetRisingCryptos(cryptos);
            var fallingCryptos = _analysisService.GetFallingCryptos(cryptos);
            var cheapestCryptos = _analysisService.GetCheapestCryptos(cryptos);
            var mostExpensiveCryptos = _analysisService.GetMostExpensiveCryptos(cryptos);

            var risingStocks = _analysisService.GetRisingStocks(stocks);
            var fallingStocks = _analysisService.GetFallingStocks(stocks);
            var cheapestStocks = _analysisService.GetCheapestStocks(stocks);
            var mostExpensiveStocks = _analysisService.GetMostExpensiveStocks(stocks);

            var viewModel = new CryptoViewModel
            {
                RisingCryptos = risingCryptos,
                FallingCryptos = fallingCryptos,
                CheapestCryptos = cheapestCryptos,
                MostExpensiveCryptos = mostExpensiveCryptos,
                RisingStocks = risingStocks,
                FallingStocks = fallingStocks,
                CheapestStocks = cheapestStocks,
                MostExpensiveStocks = mostExpensiveStocks
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AutoTrade()
        {
            var cryptos = await _cryptoService.GetCryptoCurrenciesAsync();
            var stocks = await _stockService.GetStocksAsync();

            foreach (var crypto in cryptos)
            {
                var prediction = _mlService.Predict(new MarketData
                {
                    PriceChangePercentage24h = (float)crypto.PriceChangePercentage24h,
                    MarketCap = (float)crypto.MarketCap,
                    CurrentPrice = (float)crypto.CurrentPrice
                });

                if (prediction > 0.5) // Arbitrary threshold for buying
                {
                    // Execute buy order for crypto
                }
                else if (prediction < -0.5) // Arbitrary threshold for selling
                {
                    // Execute sell order for crypto
                }
            }

            foreach (var stock in stocks)
            {
                var prediction = _mlService.Predict(new MarketData
                {
                    PriceChangePercentage24h = (float)stock.PriceChangePercentage24h,
                    MarketCap = (float)stock.MarketCap,
                    CurrentPrice = (float)stock.CurrentPrice
                });

                if (prediction > 0.5) // Arbitrary threshold for buying
                {
                    // Execute buy order for stock
                }
                else if (prediction < -0.5) // Arbitrary threshold for selling
                {
                    // Execute sell order for stock
                }
            }

            return RedirectToAction("Index");
        }
    }
}
