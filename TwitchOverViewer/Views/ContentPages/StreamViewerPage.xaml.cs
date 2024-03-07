namespace TwitchOverViewer.ContentPages;

public partial class StreamViewerPage : ContentPage
{
	public StreamViewerPage()
	{
		InitializeComponent();
	}

	public StreamViewerPage(string channelName)
	{
		InitializeComponent();
		wvStreamPlayer.Source = "https://player.twitch.tv/?channel=" + channelName + "&&parent=watchrinserepeat.com";
	}
}