////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// NavigationManager.cs
// This file contains the logic for managing navigation operations within the application
//
// Copyright (C) 2024 noahsub
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// IMPORTS
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Inferyn.Pages;
using Inferyn.Windows;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// NAMESPACE
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Inferyn.Managers;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// NAVIGATION MANAGER CLASS
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public static class NavigationManager
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // PAGES
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The pages that can be navigated to.
    /// </summary>
    private static readonly Dictionary<string, UserControl?> Pages =
        new()
        {
            { "MainPage", null },
            { "UpdatePage", null },
        };

    /// <summary>
    /// The current page.
    /// </summary>
    private static UserControl? _currentPage;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // LOADING
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Contains a dictionary of pages and whether they have been loaded.
    /// </summary>
    private static readonly Dictionary<string, bool> LoadedPages =
        new()
        {
            { "MainPage", false },
            { "UpdatePage", false },
        };

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // NAVIGATION
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Switches the page to the specified page.
    /// </summary>
    /// <param name="mainWindow">The main window.</param>
    /// <param name="page">The page to switch to.</param>
    public static void SwitchPage(MainWindow mainWindow, string page)
    {
        var pageContent = mainWindow.FindControl<ContentControl>("PageContent");
        if (pageContent == null)
            return;

        if (!Pages.ContainsKey(page))
            return;

        if (Pages[page] == null)
        {
            Pages[page] = CreatePageInstance(page);
        }

        if (!LoadedPages[page])
        {
            LoadedPages[page] = true;
            (Pages[page] as dynamic)?.OnFirstLoad();
        }

        pageContent.Content = Pages[page];
        _currentPage = Pages[page];
        (Pages[page] as dynamic)?.OnNavigatedTo();
    }

    /// <summary>
    /// Helper for creating an instance of the specified page.
    /// </summary>
    /// <param name="pageName"></param>
    /// <returns></returns>
    private static UserControl? CreatePageInstance(string pageName) =>
        pageName switch
        {
            "MainPage" => new MainPage(),
            "UpdatePage" => new UpdatePage(),
            _ => null,
        };

    /// <summary>
    /// Creates an instance of the specified page.
    /// </summary>
    /// <param name="name"></param>
    public static async Task CreatePage(string name)
    {
        Pages[name] = CreatePageInstance(name);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // GETTERS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Gets the current page.
    /// </summary>
    /// <returns></returns>
    public static UserControl? GetCurrentPage() => _currentPage;

    /// <summary>
    /// Gets the specified page.
    /// </summary>
    /// <param name="name">The name of the page to get.</param>
    /// <returns></returns>
    public static Control? GetPage(string name) =>
        Pages.ContainsKey(name) ? Pages[name] : new Control();
}
