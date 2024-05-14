namespace FinanceBot.Services
{
    public static class TechnicalIndicators
    {
        public static IEnumerable<decimal> CalculateSMA(IEnumerable<decimal> prices, int period)
        {
            var sma = prices.Select((price, index) => prices.Skip(index).Take(period).Average()).ToList();
            return sma.Skip(period - 1);
        }

        public static IEnumerable<decimal> CalculateEMA(IEnumerable<decimal> prices, int period)
        {
            var k = 2.0m / (period + 1);
            var ema = new List<decimal>();
            decimal? previousEma = null;

            foreach (var price in prices)
            {
                if (previousEma == null)
                {
                    previousEma = price;
                }
                else
                {
                    previousEma = (price * k) + (previousEma * (1 - k));
                }

                ema.Add(previousEma.Value);
            }

            return ema.Skip(period - 1);
        }

        public static IEnumerable<decimal> CalculateRSI(IEnumerable<decimal> prices, int period)
        {
            var gains = new List<decimal>();
            var losses = new List<decimal>();

            for (int i = 1; i < prices.Count(); i++)
            {
                var change = prices.ElementAt(i) - prices.ElementAt(i - 1);
                if (change > 0)
                {
                    gains.Add(change);
                    losses.Add(0);
                }
                else
                {
                    gains.Add(0);
                    losses.Add(Math.Abs(change));
                }
            }

            var avgGain = gains.Take(period).Average();
            var avgLoss = losses.Take(period).Average();

            var rs = avgGain / avgLoss;
            var rsi = 100 - (100 / (1 + rs));

            return new List<decimal> { rsi };
        }
    }
}
