using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Inferyn.Controls;

public partial class ChatInputBox : UserControl
{
    public event EventHandler<string>? SendRequested = delegate { };

    public ChatInputBox()
    {
        InitializeComponent();

        InputTextBox.AddHandler(KeyDownEvent, InputElement_OnKeyDown, RoutingStrategies.Tunnel);
    }

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            e.Handled = true;

            var message = InputTextBox.Text.TrimEnd('\r', '\n');

            if (!string.IsNullOrWhiteSpace(message))
            {
                SendRequested?.Invoke(this, message);
                InputTextBox.Text = ""; // clear after send
            }
        }
    }

    public string Text
    {
        get => InputTextBox.Text ?? "";
        set => InputTextBox.Text = value;
    }
}