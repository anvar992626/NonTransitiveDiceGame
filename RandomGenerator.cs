using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;

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
                result = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue;
            } while (result >= (int.MaxValue / range) * range);
            return result % range;
        }
    }

    public static string GenerateHMAC(string key, int message)
    {
        var hmac = new HMac(new Sha3Digest(256));
        var keyBytes = Convert.FromHexString(key); // Use byte array for cryptographic secux
                                                   // rity
        hmac.Init(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(keyBytes));
        var messageBytes = BitConverter.GetBytes(message);
        var result = new byte[hmac.GetMacSize()];
        hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
        hmac.DoFinal(result, 0);
        return BitConverter.ToString(result).Replace("-", "").ToLower();
    }
}
