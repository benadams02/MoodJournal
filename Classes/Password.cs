namespace MoodJournal.Security
{
    public class Password : MoodJournal.Security.IPassword
    {
        IPassword _ipassword;

        public Password()
        {
            _ipassword = this;
        }

        public string EncryptedPassword { get; set; }
        public string Salt { get; set; }
        string IPassword.Password { get; set; }

        public void Decrypt(string plainTextPass)
        {
            throw new NotImplementedException();
        }

        public void Decrypt(string plainTextPass, string Salt)
        {
            throw new NotImplementedException();
        }

        public void Encrypt(string plainTextPass)
        {
            throw new NotImplementedException();
        }

        public void Encrypt(string plainTextPass, string Salt)
        {
            throw new NotImplementedException();
        }
    }
}
