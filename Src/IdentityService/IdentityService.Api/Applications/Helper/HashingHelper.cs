using System.Security.Cryptography;
using System.Text;

namespace Bank.Application.Helper;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
        {
            byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != passwordHash[i])
                    return false;
            }
        }
        return true;
    }
}