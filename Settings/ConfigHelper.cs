using System.Text.Json;

namespace PlaywrightDemo.Settings
{


    public class ConfigHelper
    {
        private static readonly JsonSerializerOptions CachedJsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static JsonElement GetConfig()
        {
            var json = File.ReadAllText("appsettings.json");
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone();
        }

        // Fixed: Removed 'using' with Settings (not IDisposable) and cached JsonSerializerOptions
        public static AppSetting? GetSettings()
        {
            var json = File.ReadAllText("appsettings.json");
            return JsonSerializer.Deserialize<AppSetting>(json, CachedJsonOptions);          
        }

        //public static string? GetBaseUrl()
        //{
        //    return GetConfig().GetProperty("BaseUrl").GetString();
        //}

        //public static String? GetBrowser()
        //{
        //    return GetConfig().GetProperty("Browser").GetString();
        //}

        //public static String? GetUsername()
        //{
        //    return GetConfig().GetProperty("Username").GetString();
        //}

        //public static String? GetPassword()
        //{
        //    return GetConfig().GetProperty("Password").GetString();
        //}

        //public static String GetTimeout()
        //{
        //    return GetConfig()["Timeout"];
        //}

        //public static bool IsHeadlessMode()
        //{
        //    return GetConfig().GetProperty("IsHeadlessMode").GetBoolean();
        //}

        //public static String GetTestEnvironment()
        //{
        //    return GetConfig()["TestEnvironment"];
        //}

        //public static String GetTestDataFile()
        //{
        //    return GetConfig()["TestDataFile"];
        //}
    }
}
