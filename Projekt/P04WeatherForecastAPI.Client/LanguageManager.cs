using System;
using System.Globalization;
using System.Windows;
using System.Diagnostics;


public static class LanguageManager
{
    private static ResourceDictionary _resourceDictionary;
    private static bool _isEnglishLanguage = true; // Default language

    public static void SetLanguage()
    {
        string languageCode = _isEnglishLanguage ? "en-US" : "pl-PL";
        string uri = $"Languages/Strings.{languageCode}.xaml";
        _resourceDictionary = new ResourceDictionary
        {
            Source = new Uri(uri, UriKind.Relative)
        };
        ResourceDictionary myResourceDictionary = Application.Current.Resources.MergedDictionaries[0];
        Uri resourceUri = myResourceDictionary.Source;
        string fileName = resourceUri.OriginalString;
        if (fileName == "Languages/Strings.pl-PL.xaml" || fileName == "Languages/Strings.en-US.xaml")
            Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(_resourceDictionary);
    }

public static string GetActualLanguage()
{
    return _isEnglishLanguage ? "en-US" : "pl-PL";
}

    public static void ToggleLanguage()
    {
        _isEnglishLanguage = !_isEnglishLanguage;
        SetLanguage();
    }
}
