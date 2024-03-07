# TwitchOverViewer
 App for creating Always-On-Top windows to view twitch content. 


## Focus
The primary focus of this app is learning and practicing the following technologies:
* APIs
* .NET MAUI
* Webviews

## Code Summary
While the purpose of this app is generally for use in a Windows or Mac environment as the key features are not useful in Android and iOS enviroments the focus has been on Windows as the developer lacks the development tools for testing in Mac.

For the Windows side of the app windows can be created as Always On Top windows. This is done in the Platforms/Windows/App.xaml.cs file which affects the progarm when running the windows build.

To keep the authorization keys protected it is saved in a config.json outside of the solution; future releases will allow an encoded version of the key be saved. For now they can be entered manually the first time and retrieved afterwards.

### Twitch Viewer
Uses a webview object to display a twitch stream.

### Chat Viewer
Creates a web socket connection to Twitch. Listens for and transmits chat messages. Utilizes WeakReferenceMessenger to send the Twitch ChatMessage object to the listener that processes its contents allowing for messages, emotes and badges to be displayed.

## Dependencies
* MAUI Community Toolkit 7.0.1
* MAUI Community Toolkit Media Element 3.0.1
* MAUI Community Tookkit MVVM 8.2.2
* Microsoft Maui Controls 8.0.6
* Microsoft Maui Controls Compatibility 8.0.6
* TwithLib 3.5.3

  
