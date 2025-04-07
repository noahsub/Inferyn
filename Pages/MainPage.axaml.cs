using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Inferyn.Controls;
using Inferyn.Entities;
using Inferyn.Interfaces;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;


namespace Inferyn.Pages;

public partial class MainPage : UserControl, IPage
{
    public List<ChatMessage> History = new List<ChatMessage>();
    public String ChatId = DateTime.UtcNow.ToString("yyyy-MM-dd-HH.mm.ss.fff");
    
    public MainPage()
    {
        Initialize();
        LoadChats();
        CreateChat();
    }

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

    private string GetAddress()
    {
        return AddressTextBox.Text ?? "";
    }
    
    private string GetPort()
    {
        return PortTextBox.Text ?? "";
    }

    private string GetModel()
    {
        return ModelTextBox.Text ?? "";
    }
    
    public string GenerateId()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd-HH.mm.ss.fff");
    }

    public void DeselectChat()
    {
        foreach (var child in HistoryStackPanel.Children)
        {
            if (child is Button button)
            {
                button.Background = new SolidColorBrush((Color)Application.Current.Resources["ControlsBackground"]);
            }
        }
    }

    public void LoadChats()
    {
        // Find all existing chats
        var chatFiles = Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Inferyn"), "*.json");

        foreach (var chat in chatFiles)
        {
            var id = Path.GetFileNameWithoutExtension(chat);
            CreateChat(id:id);
        }
    }

    public void LoadChat(string id)
    {
        History.Clear();
        MessagesStackPanel.Children.Clear();
        
        ChatId = id;
        
        // Load chat history from file, the path is the user's Documents folder, in the Inferyn folder
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Inferyn", $"{id}.json");
        if (!File.Exists(filePath)) return;
        var json = File.ReadAllText(filePath);
        var chatHistory = JsonSerializer.Deserialize<List<ChatMessage>>(json);
        
        if (chatHistory == null) return;
        
        foreach (var message in chatHistory)
        {
            History.Add(message);
            var messageControl = new Message();
            messageControl.NameLabel.Content = message.Role;
            messageControl.MessageTextBox.Text = message.Content;
            MessagesStackPanel.Children.Add(messageControl);
        }
    }

    public void CreateChat(string id="")
    {
        // Deselect all chat buttons
        DeselectChat();
        
        // Clear the messages stack panel
        MessagesStackPanel.Children.Clear();
        
        // Create a new button
        Button button = new Button();
        button.HorizontalAlignment = HorizontalAlignment.Stretch;
        button.VerticalAlignment = VerticalAlignment.Stretch;
        button.VerticalContentAlignment = VerticalAlignment.Center;
        button.HorizontalContentAlignment = HorizontalAlignment.Left;
        
        // Set button background color to Static Resource Primary Accent
        button.Background = new SolidColorBrush((Color)Application.Current.Resources["PrimaryAccent"]);
        
        // Add the button to the ChatList
        HistoryStackPanel.Children.Add(button);

        if (String.IsNullOrEmpty(id))
        {
            id = GenerateId();
        }
        
        // Set button content to the existing chat ID
        button.Content = id;

        // Set the chat ID
        ChatId = id;
        
        // Click event handler
        button.Click += (sender, args) =>
        {
            // Deselect all chat buttons
            DeselectChat();
            // Select the clicked button
            button.Background = new SolidColorBrush((Color)Application.Current.Resources["PrimaryAccent"]);
            // Load the chat history
            LoadChat(id);
        };
        
        // Right-click event handler
        button.AddHandler(InputElement.PointerPressedEvent, (sender, args) =>
        {
            if (args.GetCurrentPoint(button).Properties.IsRightButtonPressed)
            {
                // Delete the file associated with the chat ID
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Inferyn", $"{id}.json");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                
                // Remove the button from the stack panel
                HistoryStackPanel.Children.Remove(button);
            }
        });
    }

    private void SaveChat(string id)
    {
        // If the directory does not exist, create it
        var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Inferyn");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        // If the file does not exist, create it
        var filePath = Path.Combine(directoryPath, $"{id}.json");
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        
        // Save the chat history to a file, the path is the user's Documents folder, in the Inferyn folder
        var json = JsonSerializer.Serialize(History);
        File.WriteAllText(filePath, json);
    }
    
    private void CreateChatButton_OnClick(object? sender, RoutedEventArgs e)
    {
        CreateChat();
    }

    public async Task Query(string prompt)
    {
        // Add the user message to the history
        History.Add(new ChatMessage(role:"user", content:prompt));
        SaveChat(ChatId);

        Message userMessage = new Message();
        userMessage.NameLabel.Content = "User";
        userMessage.MessageTextBox.Text = prompt;
        MessagesStackPanel.Children.Add(userMessage);
        
        Message replyMessage = new Message();
        replyMessage.NameLabel.Content = GetModel();
        replyMessage.MessageTextBox.Text = "...";
        MessagesStackPanel.Children.Add(replyMessage);
        
        // Create the request body
        var body = new
        {
            model = GetModel(),
            stream = true,
            messages = History
        };

        var httpClient = new HttpClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"http://{GetAddress()}:{GetPort()}/api/chat")
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
        
        using var response = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        var reply = "";
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            var jsonChunk = JsonSerializer.Deserialize<Dictionary<string, object>>(line);
            if (jsonChunk != null && jsonChunk.TryGetValue("message", out var messageObj) && messageObj is JsonElement elem)
            {
                string content = elem.GetProperty("content").GetString();

                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    replyMessage.MessageTextBox.Text += content;
                });
                await Task.Delay(10); // Try 5–20ms
                
                reply += content;
            }
        }
        
        // Add the assistant message to the history
        History.Add(new ChatMessage(role: "assistant", content: reply));
        SaveChat(ChatId);
    }

    private async void ChatInputBox_OnSendRequested(object? sender, string message)
    {
        await Query(message);
    }
}