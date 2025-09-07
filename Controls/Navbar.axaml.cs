using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Inferyn.Managers;

namespace Inferyn.Controls;

public partial class Navbar : UserControl
{
    public event EventHandler? NavbarExpanded;
    
    public Navbar()
    {
        InitializeComponent();
    }

    public void SetSelected(string selected)
    {
        var mutedTextColor = (Color)ThemeManager.GetResource("TextMuted");
        var accentColour = (Color)ThemeManager.GetResource("Accent");
        
        // Set the foreground colour of all buttons 
        var navButtons = new List<Button>
        {
            ChatPageButton,
            ModelPageButton,
            ServerPageButton,
            HistoryPageButton,
            UserPageButton,
            OptionsPageButton
        };

        foreach (var button in navButtons)
        {
            // Set colour to TextMuted in Static Resource
            button.Foreground = new SolidColorBrush(mutedTextColor);
        }
        
        switch (selected)
        {
            case "ChatPageButton":
            {
                ChatPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
            
            case "ModelPageButton":
            {
                ModelPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
            
            case "ServerPageButton":
            {
                ServerPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
            
            case "HistoryPageButton":
            {
                HistoryPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
            
            case "UserPageButton":
            {
                UserPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
            
            case "OptionsPageButton":
            {
                OptionsPageButton.Foreground = new SolidColorBrush(accentColour);
                break;
            }
        }
    }

    private void ChatPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("ChatPageButton");
    }


    private void ModelPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("ModelPageButton");
    }

    private void ServerPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("ServerPageButton");
    }

    private void HistoryPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("HistoryPageButton");
    }

    private void UserPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("UserPageButton");
    }

    private void OptionsPageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SetSelected("OptionsPageButton");
    }

    private void ExpandButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NavbarExpanded?.Invoke(this, EventArgs.Empty);
    }
}