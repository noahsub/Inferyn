using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Inferyn.Entities;
using Inferyn.Managers;
using Inferyn.Pages;
using Color = Avalonia.Media.Color;

namespace Inferyn;

public partial class App : Application
{
    public override void Initialize()
    {
        // Theme
        var resources = Current?.Resources;

        var darkTheme = new Theme();
        // darkTheme.Load("dark");
        darkTheme.Apply();
        
        resources["AcrylicMaterial"] = new ExperimentalAcrylicMaterial
        {
            BackgroundSource = AcrylicBackgroundSource.Digger,
            MaterialOpacity = (double)resources["Opacity"],
            TintColor = Colors.Black,
            TintOpacity = (double)resources["Tint"]
        };
        
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Startup logic for the application.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        var resources = Current?.Resources;
        
        // Application Startup
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // if the operating system is linux
            if (OperatingSystem.IsLinux())
            {
                // set the global opacity to 1.0
                if (resources != null)
                {
                    resources["GlobalOpacity"] = 1.0;
                }
            }

            var splashScreen = new Windows.SplashScreen();
            splashScreen.Show();

            Task.Run(async () =>
            {
                // Check for updates asynchronously
                // splashScreen.SetLoadingTextUiThread("CHECKING FOR UPDATES");
                // DataManager.LatestVersion = await WebManager.GetLatestVersion();

                // Thread.Sleep(2000);

                // Switch to the UI thread to update the UI
                Dispatcher.UIThread.Post(async () =>
                {
                    // Create application pages
                    // await NavigationManager.CreatePage(new MainPage());
                    // await NavigationManager.CreatePage(new UpdatePage());
                    await NavigationManager.CreatePage(new ChatPage());

                    // IMPORTANT: MainWindow must be created after all pages are created, otherwise,
                    // the first page will be initialized twice
                    var mainWindow = new Windows.MainWindow();
                    desktop.MainWindow = mainWindow;

                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }

        base.OnFrameworkInitializationCompleted();
    }
}