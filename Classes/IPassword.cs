namespace MoodJournal.Security
{
    public interface IPassword
    {
        string EncryptedPassword { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
        void Encrypt(string plainTextPass);
        void Encrypt(string plainTextPass, string Salt);
        void Decrypt(string plainTextPass);
        void Decrypt(string plainTextPass, string Salt);
    }
}
