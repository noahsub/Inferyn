using System.Collections.Generic;
using System.Diagnostics;
using Avalonia;

namespace Inferyn.Managers;

public class ThemeManager
{
    public static object? GetResource(string reference)
    {
        var resources = Application.Current?.Resources;
        return resources?[reference];
    }
}