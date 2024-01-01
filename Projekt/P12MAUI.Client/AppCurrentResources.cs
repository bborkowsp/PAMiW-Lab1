namespace P12MAUI.Client
{
    class AppCurrentResources
    {

        public static string Token = "";
        public static string Language = "english";
        public static bool DarkTheme = true;

        public static void LoadSettings()
        {
            Token = Preferences.Get("token", "");
            Language = Preferences.Get("language", "english");
            SetTheme(Preferences.Get("isDarkTheme", true), true);
        }

        public static void SetToken(string token)
        {
            AppCurrentResources.Token = token;

            Preferences.Set("token", token);
        }

        // THEME

        public static void ToggleTheme()
        {
            SetTheme(DarkTheme, true);
        }

        public static void SetTheme(bool DarkTheme, bool save)
        {
            AppCurrentResources.DarkTheme = DarkTheme;
            UpdateResources();

            if (save)
            {
                Preferences.Set("isDarkTheme", DarkTheme);
            }
        }

        public static void UpdateResources()
        {
            if (DarkTheme)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
        }

        // LANGUAGE

        public static void ToggleLanguage()
        {
            if (Language == "polski")
            {
                SetLanguage("english");
            }
            else
            {
                SetLanguage("polski");
            }
        }

        public static void SetLanguage(string language)
        {
            AppCurrentResources.Language = language;

            Preferences.Set("language", language);
        }

    }
}
