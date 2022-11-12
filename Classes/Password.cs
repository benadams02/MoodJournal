using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MoodJournal.Security
{
    public class Password
    {
        public bool CheckPasswordsMatch(ClaimsPrincipal user, string hashedPass, string plainTextPass)
        {
            PasswordHasher<ClaimsPrincipal> hasher = new PasswordHasher<ClaimsPrincipal>();
            var verificationResult = hasher.VerifyHashedPassword(user, hashedPass, plainTextPass);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }

        public string Hash(ClaimsPrincipal user, string plainTextPass)
        {
            PasswordHasher<ClaimsPrincipal> hasher = new PasswordHasher<ClaimsPrincipal>();
            return hasher.HashPassword(user, plainTextPass);
        }
    }
}
