namespace MoodJournal
{
    public static class Server
    {
        public static MoodJournal.Settings Settings;
        public static MoodJournal.DataSource DataSource;

        public static void Load()
        {
            Settings = MoodJournal.Settings.Load();
            DataSource = new MoodJournal.DataSource();
            DataSource.LoadConnection();
        }
    }
}
