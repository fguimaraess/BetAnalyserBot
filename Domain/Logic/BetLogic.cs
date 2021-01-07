using Domain.Model;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Domain
{
    public class BetLogic : IBetLogic
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private const string apiKey = "b08814c70aa840b5904688403cf2bd26";
        private const string apiUrl = "https://api.sportsdata.io/v3/nba/odds/json";

        public BetLogic(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<object> GetBetsToday()
        {
            SendTelegramMessage();
            //SendEmail();
            var urlWithKey = $"{apiUrl}/GameOddsByDate/{DateTime.Now.Date:yyyy-MM-dd}?key={apiKey}";
            var retorno = await _httpClient.GetAsync(urlWithKey);
            var conteudo = await retorno.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<List<SportsData>>(conteudo);
            return model;
        }

        private void SendTelegramMessage()
        {
            try
            {
                var bot = new Telegram.Bot.TelegramBotClient(_config["Services:Telegram"]);
                var chatId = new ChatId("@betBotAnalyser");
                var t = bot.SendTextMessageAsync(chatId, "Essa bagaça funciona msm!!!!").Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async void SendEmail()
        {
            var client = new SendGridClient(_config["Services:SendGrid"]);
            var from = new EmailAddress("felipeemg@gmail.com", "BET ANALYSER BOT");
            var subject = "[BOT] - Resultado das análises";
            var to = new EmailAddress("felipeemg@gmail.com", "Felipe");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Method 1
            var task = Task.Run(() => client.SendEmailAsync(msg));
            var result = task.Result;
        }
    }
}
