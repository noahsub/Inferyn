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

    private void ToggleSidebar_Click(object? sender, RoutedEventArgs e)
    {
        // Toggle sidebar first
        SidebarSplitView.IsPaneOpen = !SidebarSplitView.IsPaneOpen;

        // Update button visibility based on new state
        if (SidebarSplitView.IsPaneOpen)
        {
            ExpandSidebarButton.IsVisible = false;
            CollapseSidebarButton.IsVisible = true;
        }
        else
        {
            ExpandSidebarButton.IsVisible = true;
            CollapseSidebarButton.IsVisible = false;
        }
    }
}