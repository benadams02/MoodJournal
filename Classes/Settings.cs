using System.Text.Json;

namespace MoodJournal
{
    public class Settings
    {
        public string InstanceName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }


        public static Settings Load()
        {
            if (System.IO.File.Exists("appsettings.json"))
            {
                Settings settings;
                string appsettings = System.IO.File.ReadAllText("appsettings.json");
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(appsettings);
                settings = JsonSerializer.Deserialize<Settings>(JsonSerializer.Serialize(config["Settings"]));

                return settings;
            }
            else
            {
                return null;
            }
        }
    }
}
