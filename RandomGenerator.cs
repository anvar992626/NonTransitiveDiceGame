using System;
using System.Security.Cryptography;
using System.Text;

public class RandomGenerator
{
    public static string GenerateKey()
    {
        var key = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }
        return BitConverter.ToString(key).Replace("-", "").ToLower();
    }

    public static int GenerateRandomNumber(int range)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            int result;
            do
            {
                var randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                result = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue; // Ensure positive
            } while (result >= (int.MaxValue / range) * range); // Reject biased values
            return result % range;
        }
    }

    public static string GenerateHMAC(string key, int message)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message.ToString()));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
