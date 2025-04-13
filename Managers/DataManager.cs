using System.IO;

namespace Inferyn.Managers;

public class DataManager
{
    /// <summary>
    /// The current version of the application.
    /// </summary>
    public static string CurrentVersion { get; set; } = File.ReadAllText("VERSION");

    /// <summary>
    /// The latest version of the application.
    /// </summary>
    public static string LatestVersion { get; set; } = CurrentVersion;
}