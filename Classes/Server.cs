namespace MoodJournal
{
    public static class Server
    {
        public static MoodJournal.Settings Settings;
        public static MoodJournal.DataSource DataSource;
        public static Dictionary<string, string> ActiveSessions;

        public static void Load()
        {
            Settings = MoodJournal.Settings.Load();
            DataSource = new MoodJournal.DataSource();
            DataSource.LoadConnection();
            ActiveSessions = new Dictionary<string, string>();
        }
    }
}
