using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using System.Text.Json;
using Avalonia;
using Newtonsoft.Json;

namespace Inferyn.Entities;

public class Theme
{
    public Color WindowBackground { get; set; } = Color.Parse("#17171a");
    public Color PanelBackground { get; set; } = Color.Parse("#121214");
    public Color Accent { get; set; } = Color.Parse("#38b178");
    public Color Text { get; set; } = Color.Parse("#f3f3f3");
    public Color TextMuted { get; set; } = Color.Parse("#6c6c6c");
    public double Radius { get; set; } = 10.0;
    public CornerRadius CornerRadius { get; set; } = new(10.0);
    public double Opacity { get; set; } = 1.0;
    public double Tint { get; set; } = 1.0;
    public Color ComponentBackground { get; set; } = Color.Parse("#1e1e21");

    public void Load(string themeFileName)
    {
        var themeFilePath = Path.Combine("Themes", $"{themeFileName}.json");

        if (!File.Exists(themeFilePath))
        {
            throw new FileNotFoundException($"Theme file '{themeFilePath}' not found.");
        }

        var json = File.ReadAllText(themeFilePath);
        
        var themeValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        
        // Copy values
        WindowBackground = Color.Parse(themeValues["WindowBackground"].ToString());
        PanelBackground = Color.Parse(themeValues["PanelBackground"].ToString());
        Accent = Color.Parse(themeValues["Accent"].ToString());
        Text = Color.Parse(themeValues["Text"].ToString());
        TextMuted = Color.Parse(themeValues["TextMuted"].ToString());
        Radius = Radius;
        Opacity = Opacity;
        Tint = Tint;
        ComponentBackground = Color.Parse(themeValues["ComponentBackground"].ToString());
        CornerRadius = CornerRadius.Parse(themeValues["CornerRadius"].ToString());
    }

    // Apply theme to Avalonia resources
    public void Apply()
    {
        var resources = Avalonia.Application.Current?.Resources;
        if (resources == null) return;

        resources["WindowBackground"] = WindowBackground;
        resources["PanelBackground"] = PanelBackground;
        resources["Accent"] = Accent;
        resources["Text"] = Text;
        resources["TextMuted"] = TextMuted;
        resources["Radius"] = Radius;
        resources["Opacity"] = Opacity;
        resources["Tint"] = Tint;
        resources["ComponentBackground"] = ComponentBackground;
        resources["CornerRadius"] = CornerRadius;
    }
}