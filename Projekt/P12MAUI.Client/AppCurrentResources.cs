namespace P12MAUI.Client
{
    class AppCurrentResources
    {

        public static string Token = "";

        public static void SetToken(string token)
        {
            AppCurrentResources.Token = token;

            Preferences.Set("token", token);
        }
    }
}
