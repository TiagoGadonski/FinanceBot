using FinanceBot.Models;
using Newtonsoft.Json;

namespace FinanceBot.Services
{
    public class CryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CryptoCurrency>> GetCryptoCurrenciesAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
            return JsonConvert.DeserializeObject<List<CryptoCurrency>>(response);
        }
    }
}
