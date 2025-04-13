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
        
        NavigationManager.SwitchPage(this, "MainPage");
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