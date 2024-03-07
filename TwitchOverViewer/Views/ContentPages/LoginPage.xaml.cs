using System.Diagnostics;
using System.Text.Json;
using TwitchLib.Api;
using TwitchOverViewer.ContentPages;
using TwitchOverViewer.Controllers;
using TwitchOverViewer.Models;
using TwitchOverViewer.Views.ContentPages;

namespace TwitchOverViewer.Views.ContentPages;

public partial class LoginPage : ContentPage
{
    private static List<string> Scopes = new List<string> { "chat%3Aread", "chat%3Aedit", "user%3Aread%3Afollows" };

	public LoginPage()
	{
		InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        //Read in the config file
        string json = File.ReadAllText(Globals.configPath + "config.json");
        //Turn the json into a Config object
        var configJson = JsonSerializer.Deserialize<Config>(json);

        Config config = new Config();
        
        if (configJson != null)
        {
            
            config.OAuthToken = configJson.OAuthToken;
            config.UserRefreshToken = configJson.UserRefreshToken;
            config.UserToken = configJson.UserToken;
            config.RedirectUri = configJson.RedirectUri;
            config.ClientId = configJson.ClientId;
            config.ClientSecret = configJson.ClientSecret;

        }
        else { Debug.WriteLine("Bad Config File"); return; }
        //make sure config is valid
        try
        {
            ValidateConfig(config);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); return; }

        //create twitch api instance
        TwitchLib.Api.TwitchAPI api = new TwitchLib.Api.TwitchAPI();
        api.Settings.ClientId = config.ClientId;

        //start the local webserver
        WebServer webServer = new WebServer(config.RedirectUri);

        //get the auth url
        Debug.WriteLine($"Please Authrorize Here: \n{getAuthorizationCodeUrl(config.ClientId, config.RedirectUri, Scopes)}");
        eRedirectUrl.Text = getAuthorizationCodeUrl(config.ClientId, config.RedirectUri, Scopes);
        wvAuthenticationWindow.Source = getAuthorizationCodeUrl(config.ClientId, config.RedirectUri, Scopes);
        wvAuthenticationWindow.IsVisible = true;

        btnLogin.IsEnabled = false;

        //Listen for incoming requests
        var auth = await webServer.Listen();

        //Exchange the auth code for oauth access/refresh
        var response = await api.Auth.GetAccessTokenFromCodeAsync(auth.Code, config.ClientSecret, config.RedirectUri);

        //update api with aquired token
        api.Settings.AccessToken = response.AccessToken;

        //update config with new data
        config.UserToken = response.AccessToken;
        config.UserRefreshToken = response.RefreshToken;
        Debug.WriteLine($"User Token: {response.AccessToken}");
        Debug.WriteLine($"Refresh Token: {response.RefreshToken}");


        //Turn the config object into Json
        json = JsonSerializer.Serialize(config);
        try
        {
            //Write the Json into a File
            File.WriteAllText(Globals.configPath + "config.json", json);
        }
        catch { Debug.WriteLine("config.json write failure"); }
    }

    private static void ValidateConfig(Config config)
    {
        if (String.IsNullOrEmpty(config.OAuthToken)) throw new Exception("OAuthToken cannot be null or empty");
        if (String.IsNullOrEmpty(config.ClientId)) throw new Exception("Client ID cannot be null or empty");
        if (String.IsNullOrEmpty(config.RedirectUri)) throw new Exception("Redirect Uri cannot be null or empty");
    }

    private static string getAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes)
    {
        var scopeStr = String.Join("+", scopes);

        return "https://id.twitch.tv/oauth2/authorize?" +
            $"client_id={clientId}&" +
            $"redirect_uri={System.Web.HttpUtility.UrlEncode(redirectUri)}&" +
            "response_type=code&" +
            $"scope={scopeStr}" +
            "&State=klsjdfsldkjfkje3432";
        // TODO: Generate Random State and verify
    }

    private async void btnRefresh_Clicked(object sender, EventArgs e)
    {
        //Read in the config file
        string json = File.ReadAllText(Globals.configPath + "config.json");
        //Turn the json into a Config object
        var configJson = JsonSerializer.Deserialize<Config>(json);
        //Create config object and load values into it.
        Config config = new Config();

        if (configJson != null)
        {

            config.OAuthToken = configJson.OAuthToken;
            config.UserRefreshToken = configJson.UserRefreshToken;
            config.UserToken = configJson.UserToken;
            config.RedirectUri = configJson.RedirectUri;
            config.ClientId = configJson.ClientId;
            config.ClientSecret = configJson.ClientSecret;
        }
        else { Debug.WriteLine("Bad Config File"); return; }
        //make sure config is valid
        try
        {
            ValidateConfig(config);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); return; }
        //make sure there is a refreshtoken
        if (config.UserRefreshToken is null) { Debug.WriteLine("No Refresh Token");eRedirectUrl.Text = "No RefreshToken, try a fresh login"; return; }

        //create twitch api instance
        TwitchLib.Api.TwitchAPI api = new TwitchLib.Api.TwitchAPI();
        api.Settings.ClientId = config.ClientId;
        
        //request the new user token
        var refresh = await api.Auth.RefreshAuthTokenAsync(config.UserRefreshToken, config.ClientSecret);
        Debug.WriteLine(refresh.AccessToken);

        //update config file
        config.UserToken = refresh.AccessToken;

        //Turn the config object into Json
        json = JsonSerializer.Serialize(config);
        try
        {
            //Write the Json into a File
            File.WriteAllText(Globals.configPath + "config.json", json);
        }
        catch { Debug.WriteLine("config.json write failure"); }

    }
}