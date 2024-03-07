namespace TwitchOverViewer.ContentPages;

using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TwitchLib.Api;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Client.Models;
using TwitchOverViewer.Controllers;


public partial class ChatViewerPage : ContentPage
{
    private string? ChannelName;
	private string? ChannelId;
    private string? UserName;
	private TwitchChatManager? twitchChatManager;
    private TwitchAPIClient? twitchAPI;

    private HtmlWebViewSource viewSource = new HtmlWebViewSource
    {
		Html = "<html><body style=\"background-color:black; font-family:sans-serif\" id=\"theChat\"><p style=\"color:white;\">Chat:</p></body></html>"
	};

    public ChatViewerPage()
	{
		InitializeComponent();

        //set the source for wvChat
        wvChat.Source = viewSource;

	}

	public ChatViewerPage(string channel, string username, string key)
	{
		InitializeComponent();

		if (channel != null && username != null && key != null && channel != "" && username != "" && key != "")
		{
			twitchChatManager = new TwitchChatManager(username, key, channel);
		}
		else
		{
			btnSendMessage.Text = "Something Went Wrong";
			btnSendMessage.IsEnabled = false;
		}

        //set the twitchAPI
        twitchAPI = new TwitchAPIClient(Globals.clientId, Globals.oAuthToken);

        //Set the channelId
        if (channel != null)
        {
            SetChannelId(channel);
            ChannelName = channel;
            UserName = username;
        }    

        //set the source for wvChat
        wvChat.Source = viewSource;

        //Initialize the WeakReferenceMessenger
        WeakReferenceMessenger.Default.Register<ChatMessage>(this, HandleChatMessage);
	}

    private async void SetChannelId(string channel)
    {
        if (twitchAPI != null && channel != null)
        {
            ChannelId = await twitchAPI.GetBroadcasterId(channel);
        }
        
    }

    private void btnChatDisconnet_Clicked(object sender, EventArgs e)
	{
		if (twitchChatManager != null)
		{
			twitchChatManager.Disconnect();
		}

	}

	private void HandleChatMessage(object recipient, ChatMessage message)
	{
        ////Debug.WriteLine("A message was sent");

        ////Create hsl with content for username and message

        ////Create a line that the chat message can be placed into
        //HorizontalStackLayout chatLine = new HorizontalStackLayout { WidthRequest = 400, MaximumWidthRequest = 400 };

        ////Get the username color and convert it to a Maui color
        //Color userColor = GetColor(message.Color);

        ////Write the username and the message into labels
        //Label chatUser = new Label { Text = $"{message.Username}: ", TextColor = userColor, WidthRequest = 100 };
        //Label chatMessage = new Label { Text = message.Message, LineBreakMode = LineBreakMode.WordWrap, WidthRequest = 300 };

        ////Add the labels to the HorizontalStackLayout
        //chatLine.Children.Add(chatUser);
        //chatLine.Children.Add(chatMessage);

        ////Add the HSL to the VSL using the dispatcher since this is not the main thread.
        //vslChat.Dispatcher.Dispatch(() =>
        //{
        //	vslChat.Children.Add(chatLine);
        //});
        ////Scroll to end of the scrollview
        //svChat.Dispatcher.Dispatch(() =>
        //{
        //	svChat.ScrollToAsync(vslChat, ScrollToPosition.End, true);
        //});

        //===============================

        ////Use the WebView to display the chat with emote.

        //wvChat.Dispatcher.Dispatch(() =>
        //{
        //    viewSource.Html += $"<p><span style=\"color: {HexColor(message.ColorHex)}\"><strong> {message.Username}:</strong></span><span style=\"color: white\">  {ConvertChatMessageToHTML(message)}</span></p>";
        //    wvChat.Source = viewSource;

        //});


        //=================================

        ////Attempt to use separate webviews per line of chat
        //WebView chatLine = new WebView
        //{
        //	MinimumHeightRequest = 25,
        //	VerticalOptions = LayoutOptions.CenterAndExpand

        //};
        //HtmlWebViewSource chatMessage = new HtmlWebViewSource
        //      {
        //          Html = $"<html><body style=\" background-color: black;\"><p><span style=\"color: {HexColor(message.ColorHex)}\"><strong> {message.Username}:</strong></span><span style=\"color: white\">  {ConvertChatMessageToHTML(message)}</span></p></body></html>"
        //      }; 
        //chatLine.Source = chatMessage;
        //chatLine.MinimumWidthRequest = 400;

        //chatLine.Navigated += async (sender, args) => 
        //{
        //	try { 
        //          string script = "var style = document.createElement('style'); style.type = 'text/css'; style.innerHTML = '::-webkit-scrollbar { display: none; }'; document.getElementsByTagName('head')[0].appendChild(style);";
        //          await chatLine.EvaluateJavaScriptAsync(script);
        //	}
        //	catch (Exception ex)
        //	{
        //		// Handle exception
        //	}
        //};

        //vslChat.Dispatcher.Dispatch(new Action(() => 
        //{ 
        //	vslChat.Children.Add(chatLine);
        //	//Debug.WriteLine("Message made");
        //}));

        //=============================================

        //Get Badge info


        // Use JavaScipt to append the html
        wvChat.Dispatcher.Dispatch(async () =>
        {
            await wvChat.EvaluateJavaScriptAsync($"var element = document.getElementById(\"theChat\"); element.innerHTML += \"<p style='margin:-1px;'>{GetBadges(message)}<span style='color:{HexColor(message.ColorHex)}'><strong> {message.Username}:</strong> </span><span style='color: white'>{ConvertChatMessageToHTML(message)}</span></p>\";");
            await wvChat.EvaluateJavaScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        });

        
    }

    private Microsoft.Maui.Graphics.Color GetColor (System.Drawing.Color color)
	{
		return Microsoft.Maui.Graphics.Color.FromRgb(color.R, color.G, color.B);
	}

	private string HexColor (string color)
	{
        //check if the hexcolor is null
        if (color == null) { return "white"; }
        //check if hexcolor is empty string
        else if (color == "") { return "#00FF7F"; }
        else return color;
	}

	private string ConvertChatMessageToHTML(ChatMessage message)
	{
        string messageSanatized = SanatizeChat( message.Message);


		string messageWithEmotes = messageSanatized;
		foreach (var emote in message.EmoteSet.Emotes)
		{
			string emoteImgUrl = emote.ImageUrl;
			messageWithEmotes = messageWithEmotes.Replace(emote.Name, $"<img src='{emoteImgUrl}' />");
		}

		return messageWithEmotes;
	}

    private void wvChat_Navigated(object sender, WebNavigatedEventArgs e)
    {
		//wvChat.Dispatcher.Dispatch(async () =>
		//{
		//	await wvChat.EvaluateJavaScriptAsync("window.scrollTo(0, document.body.scrollHeight);");

		//});
	}

    //Code to disconenct from chat when closing the window
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (twitchChatManager != null)
        {
            twitchChatManager.Disconnect();
        }

		WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    // This doesn't work as the url for badges has changed. Need to use the API directly.
    private string GetBadges (ChatMessage message)
    {
        string badges = "";

        foreach (var badge in message.Badges)
        {

            //Run function for type of badge
            badges += $"<img src='{Task.Run(() => twitchAPI?.GetChannelBadgeInfo(ChannelId, badge)).Result}' />";
            badges += $"<img src='{Task.Run(() => twitchAPI?.GetGlobalBadgeInfo(badge)).Result}' />";
        }

        return badges;
    }

    private void btnSendMessage_Clicked(object sender, EventArgs e)
    {
        if (entryChat.Text != null && entryChat.Text != "" && ChannelName != null)
        {
            

            //Send the message
            twitchChatManager.SendMessage(ChannelName, entryChat.Text);

            //Sanatize message
            string message = entryChat.Text;            
            message = SanatizeChat(message);

            //Make the acknowledge the message in chat
            wvChat.Dispatcher.Dispatch(async () =>
            {
                await wvChat.EvaluateJavaScriptAsync($"var element = document.getElementById(\"theChat\"); element.innerHTML += \"<p style='color:white; margin:-1px;'><strong><span style='color:goldenrod'>{UserName}:</span></strong> {message}</p>\";");
                await wvChat.EvaluateJavaScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            });

            //Clear the entry
            entryChat.Text = "";

        }
    }

    private string SanatizeChat(string message)
    {
        string cleanMessage = message.Replace("\"", "&quot;");
        cleanMessage = cleanMessage.Replace("\'", "&apos;");
        cleanMessage = cleanMessage.Replace("\\", "&bsol;");
        cleanMessage = cleanMessage.Replace("<script>", "***");
        cleanMessage = cleanMessage.Replace("<Script>", "***");
        cleanMessage = cleanMessage.Replace("<", "&LT;"); 
        cleanMessage = cleanMessage.Replace(">", "&GT;");
        cleanMessage = cleanMessage.Replace("&", "&amp;");

        return cleanMessage;
    }

}