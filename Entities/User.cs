namespace Inferyn.Entities;

public class User
{
    public string Username { get; set; }
    public string Name { get; set; }
    
    public User(string username, string name)
    {
        Username = username;
        Name = name;
    }
}