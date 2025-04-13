using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Inferyn.Managers;
using Inferyn.Interfaces;
using Inferyn.Managers;

namespace Inferyn.Pages;

public partial class UpdatePage : UserControl, IPage
{
    public UpdatePage()
    {
        Initialize();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // EVENTS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// When the later button is clicked, switch to the requirements page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LaterButton_OnClick(object? sender, RoutedEventArgs e)
    {
        // Switch to the requirements page
        var mainWindow = (Windows.MainWindow)this.VisualRoot!;
        NavigationManager.SwitchPage(mainWindow, "MainPage");
    }

    /// <summary>
    /// When the update button is clicked, open the latest release page and exit the application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        // TODO: Change url to the actual repository
        // Open the latest release page
        WebManager.OpenUrl("https://github.com/noahsub/Orthographic-Renderer/releases/latest");
        // Exit the application
        Environment.Exit(0);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // IPAGE INTERFACE IMPLEMENTATION
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Initializes the update page.
    /// </summary>
    public void Initialize()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Called when the page is first loaded by the user.
    /// </summary>
    public void OnFirstLoad() { }

    /// <summary>
    /// Called when the page is navigated to.
    /// </summary>
    public void OnNavigatedTo() { }
}