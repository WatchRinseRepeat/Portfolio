using System.Diagnostics;
using System.Text.Json;
using TwitchOverViewer.ContentPages;
using TwitchOverViewer.Controllers;
using TwitchOverViewer.Models;
using TwitchOverViewer.Views.ContentPages;

namespace TwitchOverViewer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnLaunchViewer_Clicked(object sender, EventArgs e)
        {
            Globals.isOnTopEnabled = true;
            if (eChannel.Text != null && eChannel.Text != "") 
            {
                Window streamWindow = new(new StreamViewerPage(eChannel.Text));

                streamWindow.Width = 700;
                streamWindow.Height = 400;

                Application.Current?.OpenWindow(streamWindow);
                Globals.isOnTopEnabled = false;
            }
            else
            {
                Window streamWindow = new(new StreamViewerPage());

                streamWindow.Width = 700;
                streamWindow.Height = 400;

                if (Application.Current != null)
                {
                    Application.Current.OpenWindow(streamWindow);
                }
                Globals.isOnTopEnabled = false;
            }

            //TwitchAPI twitchAPI = new TwitchAPI(eClientId.Text, eOauth.Text);
            //Globals.oAuthToken = eOauth.Text;
            //Globals.clientId = eClientId.Text;
            //Globals.userKey = eKey.Text;
            //var broadcasterId = await twitchAPI.GetBroadcasterId(eChannel.Text);

            //var badgeInfo = await twitchAPI.GetChannelBadgeInfo(broadcasterId);
        }

        //private void btnOnTop_Clicked(object sender, EventArgs e)
        //{
        //    if (Globals.isOnTopEnabled)
        //    {
        //        Globals.isOnTopEnabled = false;
        //        btnOnTop.Text = "Turn on On Top";
        //    }
        //    else
        //    {
        //        Globals.isOnTopEnabled = true;
        //        btnOnTop.Text = "Toggle OnTop Off";
        //    }
        //}

        private void btnLaunchChat_Clicked(object sender, EventArgs e)
        {
            //TwitchAPI twitchAPI = new TwitchAPI(eClientId.Text, eOauth.Text);
            //Globals.oAuthToken = eOauth.Text;
            //Globals.clientId = eClientId.Text;
            //Globals.userKey = eKey.Text;


            Globals.isOnTopEnabled = true;
            Window chatWindow = new Window(new ChatViewerPage(eChannel.Text,eUsername.Text, Globals.userKey));
            if (Application.Current != null)
            {
                chatWindow.Width = 400;
                chatWindow.Height = 550;
                Application.Current.OpenWindow(chatWindow);
            }
            Globals.isOnTopEnabled = false;

            
        }

        private async void GetFollowed(string UserName, string userToken, string clientId)
        {
            TwitchAPIClient twitchAPI = new TwitchAPIClient(clientId, userToken);
            List<TwitchChannel> channels = new List<TwitchChannel>();

            string userId = await Task.Run(() => twitchAPI.GetBroadcasterId(UserName).Result);

            try
            {
                channels = await Task.Run(() => twitchAPI.GetFollowedChannels(userId).Result);
                //Do something with the list:
                
            }
            catch { Debug.WriteLine("Failed to get Followed Channels"); }

            //Reset the Following Panel
            FollowingPanel.Children.Clear();
            FollowingPanel.ColumnDefinitions.Clear();

            //set up columns for 
            for (int i = 0; i < 4; i++)
            {
                FollowingPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star)});
            }

            int columnIndex = 0;
            int rowIndex = 0;

            //Create the channel icons and labels
            foreach(TwitchChannel channel in channels)
            {
                VerticalStackLayout followedChannel = new VerticalStackLayout();
                Image channelImage = new Image 
                { 
                    WidthRequest = 150, HeightRequest = 150,
                };
                Label channelName = new Label
                {
                    TextColor = Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 255),
                    HorizontalOptions = LayoutOptions.Center,
                    //FontSize = 18
                };

                //Debug.WriteLine(channel.thumbnail_url);

                //string? thumbUrl = channel.thumbnail_url;
                //thumbUrl = thumbUrl.Replace("{width}x{height}", "200x120");

                string thumbUrl = await Task.Run(() => twitchAPI.GetBroadcasterProfilePicture(channel.user_login));

                channelImage.Source = thumbUrl;
                channelName.Text = channel.user_name;

                //add the image and lable to the layout
                followedChannel.Children.Add(channelImage);
                followedChannel.Children.Add(channelName);

                followedChannel.GestureRecognizers.Add(new TapGestureRecognizer() 
                {
                    Command = new Command (()=>
                    {
                        eChannel.Text = channel.user_name;
                    })
                });

                //Add the VerticalStackLayout to the grid
                if (columnIndex > 3)
                {
                    columnIndex = 0;
                    rowIndex++;
                }

                if (FollowingPanel.RowDefinitions.Count <= rowIndex)
                {
                    FollowingPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                }

                FollowingPanel.Children.Add(followedChannel);
                FollowingPanel.SetRow(followedChannel, rowIndex);
                FollowingPanel.SetColumn(followedChannel, columnIndex);

                columnIndex++;
                
            }

            
        }

        private void btnGetFollowed_Clicked(object sender, EventArgs e)
        {
           GetFollowed(eUsername.Text, Globals.userKey, Globals.clientId);
        }

        private void btnOpenAuthentication_Clicked(object sender, EventArgs e)
        {
            Window authWindow = new Window(new AuthenticationPage());
            Application.Current?.OpenWindow(authWindow);
        }

        private void btnLoadConfiguration_Clicked(object sender, EventArgs e)
        {
//#if WINDOWS
            //Read in the config file
            string json = File.ReadAllText(Globals.configPath + "config.json");
            //Turn the json into a Config object
            var config = JsonSerializer.Deserialize<Config>(json);

            if (config != null)
            {
                Globals.oAuthToken = config.OAuthToken;
                Globals.userKey = config.UserToken;
                Globals.clientId = config.ClientId;

            }
            else
            {
                btnLoadConfiguration.Text = "Error";
            }
//#endif
        }

        private void btnOpenLogin_Clicked(object sender, EventArgs e)
        {
            Window loginWindow = new Window(new LoginPage());
            Application.Current?.OpenWindow(loginWindow);
        }
    }

}
