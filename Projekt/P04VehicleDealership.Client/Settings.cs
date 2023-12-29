using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P04VehicleDealership.Client
{
    class Settings
    {
        public static bool IsDarkThemeEnabled = false;

        public static void ApplyTheme()
        {
            Uri themeUri = new Uri("Themes/" + (IsDarkThemeEnabled ? "Dark" : "Light") + "Theme.xaml", UriKind.Relative);

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = themeUri });
        }

        public static void ToggleTheme()
        {
            SetTheme(!IsDarkThemeEnabled);
        }

        public static void SetTheme(bool enableDarkTheme)
        {
            IsDarkThemeEnabled = enableDarkTheme;
            ApplyTheme();
        }


    }
}