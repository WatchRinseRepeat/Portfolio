using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Markup;
using Microsoft.Maui.Controls.PlatformConfiguration;
using TwitchOverViewer.Controllers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TwitchOverViewer.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        //protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<TwitchOverViewer.App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                //.UseMauiCommunityToolkit()
                // Initialize the .NET MAUI Community Toolkit MediaElement by adding the below line of code
                //.UseMauiCommunityToolkitMediaElement()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
                    events.AddWindows(windows =>
                    {
                        windows.OnWindowCreated(xamlWindow =>
                        {
                            var window = xamlWindow as MauiWinUIWindow;
                            var MainWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);

                            var windowId = Win32Interop.GetWindowIdFromWindow(MainWindowHandle);
                            var appWindow = AppWindow.GetFromWindowId(windowId);
                            var presenter = appWindow.Presenter as OverlappedPresenter;

                            

                            //Set on top if the globals.isOnTopEnabled is true
                            appWindow.SetPresenter(AppWindowPresenterKind.Default);
                            if (Globals.isOnTopEnabled) 
                            {
                                presenter.IsAlwaysOnTop = true;
                                presenter.SetBorderAndTitleBar(false, false);

                                //Set it to extend content into the title bar so that the controls still appear
                                xamlWindow.ExtendsContentIntoTitleBar = true;

                                //Set the title bar color black
                                Microsoft.UI.Windowing.AppWindowTitleBar titleBar = appWindow.TitleBar;
                                titleBar.ForegroundColor = Microsoft.UI.Colors.Black;
                            }
                            
                        });
                    });
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

}
