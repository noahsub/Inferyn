using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Inferyn.Interfaces;
using Inferyn.Managers;
using Inferyn.Windows;

namespace Inferyn.Pages;

public partial class WelcomePage : UserControl, IPage
{
    public WelcomePage()
    {
        Initialize();
    }

    public string NavigationName { get; } = "WelcomePage";
    
    public void Initialize()
    {
        InitializeComponent();
    }

    public void OnFirstLoad()
    {
        
    }

    public void OnNavigatedTo()
    {
        
    }

    private void TabNavButton_OnClick(object? sender, RoutedEventArgs e)
    {
        // Increment the selected index of the ProfileTabControl by 1, i.e., move to the next tab
        ProfileTabControl.SelectedIndex += 1;

        if (ProfileTabControl.SelectedIndex == 4)
        {
            SummaryUsernameLabel.Content = UsernameTextBox.Text;
            SummaryNicknameLabel.Content = NameTextBox.Text;
            SummaryProfileImage.Source = ProfileImage.Source;
        }
    }

    private void SummaryTabNavButton_OnClick(object? sender, RoutedEventArgs e)
    {
        // Go to the main chat page
        var mainWindow = (MainWindow)this.VisualRoot!;
        NavigationManager.SwitchPage(mainWindow, "ChatPage");
        
        // Create profile directory for the user
        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
        {
            return;
        }
        
        FileManager.CreateDirectory(Path.Combine(FileManager.GetDataDirectory(), "Profiles", UsernameTextBox.Text ?? ""));
        
        // Save the user data
        var username = UsernameTextBox.Text ?? "";
        var name = NameTextBox.Text ?? "";
        var profilePicturePath = ProfileImagePathTextBox.Text ?? "";
        UserManager.CreateUser(username, name, profilePicturePath);
    }

    private void BrowseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = (TopLevel)this.VisualRoot!;
        FileManager.BrowseForFileAsync(topLevel, FileManager.GetDataDirectory(), new []{"*.png", "*.jpg", "*.jpeg"}).ContinueWith(t =>
        {
            if (t.Result != null)
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    var profileImagePath = t.Result;
                    ProfileImagePathTextBox.Text = profileImagePath;
                    ProfileImage.Source = new Avalonia.Media.Imaging.Bitmap(profileImagePath);
                });
            }
        });
    }
}