using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.EventSub.Websockets.Client;

using TwitchOverViewer.ContentPages;

using CommunityToolkit.Mvvm.Messaging;

namespace TwitchOverViewer.Controllers
{
    public class TwitchChatManager
    {
        private TwitchClient client;



        public TwitchChatManager(string username, string oauthToken, string channel)
        {
            //Create Credentials object // not really needed as I'm passsing them the parts of the credentials to the Constructor
            ConnectionCredentials credentials = new ConnectionCredentials(username, oauthToken);

            //create the client options
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };

            //Make the websocket client
            WebSocketClient customClient = new(clientOptions);

            //Create the twitch client
            client = new TwitchClient(customClient);
            //Log in
            client.Initialize(credentials, channel);

            ////Initialize the WeakReferenceMessenger
            //WeakReferenceMessenger.Default.Register<ChatMessage>(this, HandleChatMessage);

            //Setup method for messages receieved
            client.OnMessageReceived += Client_OnMessageReceieved;
            client.OnLog += Client_OnLog;
            //Connect
            client.Connect();
        }

        //private void HandleChatMessage(object recipient, ChatMessage message)
        //{
        //    Debug.WriteLine("A message was sent");
        //}
        private void Client_OnLog(object? sender, OnLogArgs e)
        {
#if DEBUG
            //Debug.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
#endif
        }

        private void Client_OnMessageReceieved(object? sender, OnMessageReceivedArgs e)
        {
            //Handle incoming chat messages here
            Debug.WriteLine($"[{e.ChatMessage.Username}]: {e.ChatMessage.Message}");


            //Send to messenger
            WeakReferenceMessenger.Default.Send(e.ChatMessage);

        }

        public void SendMessage(string channel, string message)
        {
            client.SendMessage(channel, message);
        }

        public void Disconnect()
        {
            client.Disconnect();
            Debug.WriteLine("Disconnected");
        }

    }
}
