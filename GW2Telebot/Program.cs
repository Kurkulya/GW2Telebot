using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;

namespace GW2Telebot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("364845520:AAGD9uv5MZ0q3m94AdlNbxbxrNT_pCnuuiA");

        static void Main(string[] args)
        {
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnReceiveError += BotOnReceiveError;
            SendMessageAboutServer();
            

            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;

            Bot.StartReceiving();
            
            Console.ReadLine();
            Bot.StopReceiving();
        }


        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async static void SendMessageAboutServer()
        {
           await SendServerPopulationAsync();
        }

        private async static Task SendServerPopulationAsync()
        {
            while (true)
            {
                await Bot.SendTextMessageAsync(399144661, "dd");
                Thread.Sleep(5000);
            }
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            if (message.Text.StartsWith("/status")) // send inline keyboard
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "dd");
            }
            else
            {
                await Bot.SendTextMessageAsync(message.Chat.Id,"ddqw");
            }
        }

        private static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
