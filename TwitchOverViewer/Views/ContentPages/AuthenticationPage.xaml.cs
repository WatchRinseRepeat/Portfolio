using System.IO;
using System.Text.Json;
using TwitchOverViewer.Controllers;

namespace TwitchOverViewer.Views.ContentPages;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage()
	{
		InitializeComponent();
	}

    private void btnLoadConfig_Clicked(object sender, EventArgs e)
    {
        //Read in the config file
        string json = File.ReadAllText(Globals.configPath + "config.json");
        //Turn the json into a Config object
        var config = JsonSerializer.Deserialize<Config>(json);

        if (config != null )
        {
            eUserToken.Text = config.UserToken;
            eOAuthToken.Text = config.OAuthToken;
            eClientId.Text = config.ClientId;
            eClientSecret.Text = config.ClientSecret;
            eRedirectionUri.Text = config.RedirectUri;
            eUserRefreshToken.Text = config.UserRefreshToken;

            Globals.oAuthToken = config.OAuthToken;
            Globals.userKey = config.UserToken;
            Globals.clientId = config.ClientId;

        }        
        else
        {
            btnLoadConfig.Text = "Error";
        }
    }

    private void btnSaveConfig_Clicked(object sender, EventArgs e)
    {
        //Make a Config object with the text entry
        var config = new Config
        {
            UserToken = eUserToken.Text,
            OAuthToken = eOAuthToken.Text,
            ClientId = eClientId.Text,
            ClientSecret = eClientSecret.Text,
            RedirectUri = eRedirectionUri.Text,
            UserRefreshToken = eUserRefreshToken.Text,
        };

        //Turn the config object into Json
        string json = JsonSerializer.Serialize(config);
        try
        {
            //Write the Json into a File
            File.WriteAllText(Globals.configPath + "config.json",  json);

            //Set the Global Variables
            Globals.oAuthToken = eOAuthToken.Text;
            Globals.clientId = eClientId.Text;
            Globals.userKey = eUserToken.Text;

            btnSaveConfig.Text = "Saved";
        }
        catch { btnSaveConfig.Text = "Failure"; }


    }
}