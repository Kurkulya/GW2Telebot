using System;
using System.Collections.Generic;
using System.Threading;
using System.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;
using System.Net;

namespace GW2Telebot
{
    class GW2Bot
    {
        public string Token {get; private set;}
        private Dictionary<string,string> commands;
        private readonly TelegramBotClient Bot;

        private int chatId_Yarik;
        private int chatId_Vlad;

        public GW2Bot(string token)
        {
            Token = token;
            Bot = new TelegramBotClient(Token);
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            chatId_Vlad = 399144661;
            chatId_Yarik = 284343759;
            commands = new Dictionary<string, string>();
            commands.Add("/start","Bot information");
            commands.Add("/help","List of commands");
            commands.Add("/status","Get server's population");
        }

        public void StartBot()
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnReceiveError += BotOnReceiveError;
            
            SendServerPopulationAsync();

            Bot.StartReceiving();   
            Console.ReadLine();
            Bot.StopReceiving();
        }  

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            throw new NotImplementedException();
        }


        private async Task SendServerPopulationAsync()
        {
            while (true)
            {
                string responsetext = await new WebClient().DownloadStringTaskAsync(new Uri("https://api.guildwars2.com/v2/worlds?ids=2012"));   
                JsonValue json = JsonValue.Parse(responsetext);
                string population = json[0]["population"];
                if (population != "Full")
                {
                    await Bot.SendTextMessageAsync(chatId_Vlad, "Sever is open, hurry up!");
                    await Bot.SendTextMessageAsync(chatId_Yarik, "Sever is open, hurry up!");
                }          
                Thread.Sleep(30 * 1000);
            }
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            if (message.Text.StartsWith("/status"))
            {
                string responsetext = new WebClient().DownloadString("https://api.guildwars2.com/v2/worlds?ids=2012");
                JsonValue json = JsonValue.Parse(responsetext);
                string population = json[0]["population"];
                await Bot.SendTextMessageAsync(message.Chat.Id, "Your server is " + population);
            }
            else if(message.Text.StartsWith("/help"))
            {
                string answer = "";
                foreach(KeyValuePair<string, string> keyValue in commands)
                {
                    answer += keyValue.Key + " - " + keyValue.Value + "\n";
                }
                await Bot.SendTextMessageAsync(message.Chat.Id,answer);
            }
            else if(message.Text.StartsWith("/start"))
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "I show server's population in Guild Wars 2");
            }
            else
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "I don't understand what do you want from me, pls use commands (/help)");
            }
        }

    }
    class Program
    { 
        static void Main(string[] args)
        {
            GW2Bot bot = new GW2Bot("364845520:AAGD9uv5MZ0q3m94AdlNbxbxrNT_pCnuuiA");
            bot.StartBot();
        }  
    }
}
