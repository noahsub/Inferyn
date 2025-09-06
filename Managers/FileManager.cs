using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Inferyn.Windows;

namespace Inferyn.Managers;

public class FileManager
{
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static string GetDataDirectory()
    {
        // return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "Inferyn");
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Inferyn");
    }
    
    public static async Task<string?> BrowseForFileAsync(TopLevel topLevel, string directoryPath, string[] filters)
    {
        var fileType = new FilePickerFileType("Custom Filter")
        {
            Patterns = filters
        };

        var options = new FilePickerOpenOptions
        {
            Title = "Select a file",
            AllowMultiple = false,
            SuggestedStartLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(directoryPath),
            FileTypeFilter = new List<FilePickerFileType> { fileType }
        };

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(options);
        if (files.Count < 1)
            return null;

        return files[0].Path.LocalPath;
    }
}