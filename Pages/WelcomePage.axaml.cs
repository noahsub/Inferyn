using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Inferyn.Interfaces;

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
}