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
        SidebarSplitView.IsPaneOpen = !SidebarSplitView.IsPaneOpen;
    }


}