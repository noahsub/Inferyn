using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Inferyn.Controls;

public partial class Server : UserControl
{
    public Server()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        OptionsPopup.PlacementTarget = this;
        OptionsPopup.IsOpen = !OptionsPopup.IsOpen;
    }

    private void OptionsPopup_OnOpened(object? sender, EventArgs e)
    {
        // Set the bl and br corner radius of the background border to 0
        BackgroundBorder.CornerRadius = new CornerRadius(10, 10, 0, 0);
    }

    private void OptionsPopup_OnClosed(object? sender, EventArgs e)
    {
        // Set the corner radius of the background border back to 10
        BackgroundBorder.CornerRadius = new CornerRadius(10);
    }
}