using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using System.Text.Json;
using Newtonsoft.Json;

namespace Inferyn.Entities;

public class Theme
{
    public Color WindowBackground { get; set; } = Color.Parse("#17171a");
    public Color PanelBackground { get; set; } = Color.Parse("#121214");
    public Color AccentColour { get; set; } = Color.Parse("#38b178");
    public double CornerRadius { get; set; } = 10.0;
    public double Opacity { get; set; } = 1.0;
    public double Tint { get; set; } = 1.0;
    
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
        AccentColour = Color.Parse(themeValues["AccentColour"].ToString());
        CornerRadius = CornerRadius;
        Opacity = Opacity;
        Tint = Tint;
    }

    // Apply theme to Avalonia resources
    public void Apply()
    {
        var resources = Avalonia.Application.Current?.Resources;
        if (resources == null) return;

        resources["WindowBackground"] = WindowBackground;
        resources["PanelBackground"] = PanelBackground;
        resources["AccentColour"] = AccentColour;
        resources["CornerRadius"] = CornerRadius;
        resources["Opacity"] = Opacity;
        resources["Tint"] = Tint;
    }
}