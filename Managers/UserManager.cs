using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.JavaScript;

namespace Inferyn.Managers;

public class UserManager
{
    public static string? CurrentUser { get; set; } = null;
    
    public static void CreateUser(string username, string name, string profilePicturePath)
    {
        // Get the profile directory for the user
        var profileDirectory = Path.Combine(FileManager.GetDataDirectory(), "Profiles", username);
        // Create the profile directory if it doesn't exist
        FileManager.CreateDirectory(profileDirectory);
        // Create a user file in the profile directory
        var userFilePath = Path.Combine(profileDirectory, "user.json");
        // Write the user data to the user file using Newtonsoft.Json
        var userData = new
        {
            Username = username,
            Name = name
        };
        var userDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(userData, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(userFilePath, userDataJson);
        // Copy the profile picture to the profile directory
        File.Copy(profilePicturePath, Path.Combine(profileDirectory, "profile.png"));
    }

    public static List<string> FindUsers()
    {
        var usernames = new List<string>();
        // Get the profile directory containing all user profiles
        var profileDirectory = Path.Combine(FileManager.GetDataDirectory(), "Profiles");
        // Read the directories in the profile directory, which represent the username of each user
        foreach (var directory in Directory.GetDirectories(profileDirectory))
        {
            usernames.Add(directory);
        }

        return usernames;
    }
}