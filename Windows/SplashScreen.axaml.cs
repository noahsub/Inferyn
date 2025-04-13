using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Inferyn.Managers;

namespace Inferyn.Windows;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();
        
        // Set the image
        var images = new List<string>();

        // for (int i = 1; i < 10; i++)
        // {
        //     images.Add($"Assets/Images/Backgrounds/inferyn_large_{i}.png");
        // }
        
        images.Add($"Assets/Images/Backgrounds/inferyn_large_1.png");

        var random = new Random();
        var image = images[random.Next(images.Count)];
        LoadingImage.Source = new Bitmap(image);
        // Set the version
        VersionLabel.Content = $"v{DataManager.CurrentVersion}";
        // Add the window to the list of open windows
        WindowManager.AddWindow(this);
    }

    /// <summary>
    /// Removes the window from the list of open windows when it is closed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        // Remove the window from the list of open windows
        WindowManager.RemoveWindow(this);
    }
    
    public void SetLoadingText(string text)
    {
        LoadingLabel.Content = text;
    }

    public void SetLoadingTextUiThread(string text)
    {
        Dispatcher.UIThread.Post(() =>
        {
            LoadingLabel.Content = text;
        });
    }
}