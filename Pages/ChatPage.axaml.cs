using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Inferyn.Interfaces;

namespace Inferyn.Pages;

public partial class ChatPage : UserControl, IPage
{
    public ChatPage()
    {
        Initialize();
    }

    public string NavigationName { get; } = "ChatPage";
    
    public void Initialize()
    {
        InitializeComponent();
    }

    public void OnFirstLoad()
    {
        return;
    }

    public void OnNavigatedTo()
    {
        // Navbar.SetSelected("ChatPageButton");
    }

    private void ToggleSidebar()
    {
        // Toggle sidebar first
        SidebarSplitView.IsPaneOpen = !SidebarSplitView.IsPaneOpen;

        // Update button visibility based on new state
        if (SidebarSplitView.IsPaneOpen)
        {
            Navbar.ExpandButton.IsVisible = false;
        }
        else
        {
            Navbar.ExpandButton.IsVisible = true;
        }
    }

    private void Navbar_OnNavbarExpanded(object? sender, EventArgs e)
    {
        ToggleSidebar();
    }

    private void CollapseSidebarButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ToggleSidebar();
    }
}