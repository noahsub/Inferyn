using System;
using Avalonia.Controls;
using Avalonia.Input;
using Inferyn.Managers;

namespace Inferyn.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // Add the window to the list of open windows
        WindowManager.AddWindow(this);

        // If there are no users, go to the welcome page
        if (UserManager.FindUsers().Count == 0)
        {
            NavigationManager.SwitchPage(this, "WelcomePage");
        }

        // Otherwise, go to the chat page
        else
        {
            NavigationManager.SwitchPage(this, "ChatPage");
            // TODO: Program currently only supports one user, so the current user is set as the first user found, 
            // but in the future, a user selection screen should be implemented
            UserManager.CurrentUser = UserManager.FindUsers()[0];
        }
    }
    
    /// <summary>
    /// If the user clicks on the top of the window, move the window.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // if cursor is on within the first 30 pixels of the window, move the window
        if (e.GetCurrentPoint(this).Position.Y < 30)
        {
            BeginMoveDrag(e);
        }
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
}