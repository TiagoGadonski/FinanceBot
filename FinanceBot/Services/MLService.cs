using FinanceBot.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace FinanceBot.Services
{
    public class MLService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public MLService()
        {
            _mlContext = new MLContext();
        }

        public void TrainModel(IEnumerable<CryptoCurrency> cryptos, IEnumerable<Stock> stocks)
        {
            var data = cryptos.Select(c => new MarketData
            {
                PriceChangePercentage24h = c.PriceChangePercentage24h,
                MarketCap = c.MarketCap,
                CurrentPrice = c.CurrentPrice,
                SMA = TechnicalIndicators.CalculateSMA(cryptos.Select(x => x.CurrentPrice), 14).Last(),
                EMA = TechnicalIndicators.CalculateEMA(cryptos.Select(x => x.CurrentPrice), 14).Last(),
                RSI = TechnicalIndicators.CalculateRSI(cryptos.Select(x => x.CurrentPrice), 14).Last()
            }).Concat(stocks.Select(s => new MarketData
            {
                PriceChangePercentage24h = s.PriceChangePercentage24h,
                MarketCap = s.MarketCap,
                CurrentPrice = s.CurrentPrice,
                SMA = TechnicalIndicators.CalculateSMA(stocks.Select(x => x.CurrentPrice), 14).Last(),
                EMA = TechnicalIndicators.CalculateEMA(stocks.Select(x => x.CurrentPrice), 14).Last(),
                RSI = TechnicalIndicators.CalculateRSI(stocks.Select(x => x.CurrentPrice), 14).Last()
            }));

            var dataView = _mlContext.Data.LoadFromEnumerable(data);
            var pipeline = _mlContext.Transforms.Concatenate("Features", "PriceChangePercentage24h", "MarketCap", "CurrentPrice", "SMA", "EMA", "RSI")
                .Append(_mlContext.Regression.Trainers.FastTree());

            _model = pipeline.Fit(dataView);
        }

        public float Predict(MarketData data)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MarketData, MarketDataPrediction>(_model);
            var prediction = predictionEngine.Predict(data);
            return prediction.Score;
        }
    }

    public class MarketData
    {
        public float PriceChangePercentage24h { get; set; }
        public float MarketCap { get; set; }
        public float CurrentPrice { get; set; }
        public float SMA { get; set; }
        public float EMA { get; set; }
        public float RSI { get; set; }
    }

    public class MarketDataPrediction
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}

