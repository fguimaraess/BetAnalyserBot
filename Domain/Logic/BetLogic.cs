using Domain.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain
{
    public class BetLogic : IBetLogic
    {
        private readonly HttpClient _httpClient;
        private const string apiKey = "b08814c70aa840b5904688403cf2bd26";
        private const string apiUrl = "https://api.sportsdata.io/v3/nba/odds/json";

        public BetLogic(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetBetsToday()
        {
            var urlWithKey = $"{apiUrl}/GameOddsByDate/{DateTime.Now.Date:yyyy-MM-dd}?key={apiKey}";
            var retorno = await _httpClient.GetAsync(urlWithKey);
            var conteudo = await retorno.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<List<SportsData>>(conteudo);
            return model;
        }
    }
}
