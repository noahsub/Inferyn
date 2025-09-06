using System.IO;

namespace Inferyn.Managers;

public class UserManager
{
    public string? CurrentUser { get; set; } = null;
    
    public void CreateUser(string username, string name)
    {
        FileManager.CreateDirectory(Path.Combine(FileManager.GetDataDirectory(), username));
    }
}