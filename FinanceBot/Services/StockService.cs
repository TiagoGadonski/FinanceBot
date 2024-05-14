using FinanceBot.Models;
using Newtonsoft.Json;

namespace FinanceBot.Services
{
    public class StockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Stock>> GetStocksAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.fictitiousstocks.com/v1/markets");
            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }
    }
}
