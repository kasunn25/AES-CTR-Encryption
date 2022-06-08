using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AES_CTR_KeyGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            /*            var key = new byte[16];
                        RandomNumberGenerator.Create().GetBytes(key);
                        var nonce = new byte[8];
                        RandomNumberGenerator.Create().GetBytes(nonce);*/

            byte[] key = Encoding.UTF8.GetBytes("bHPX/WZBL2Ci90nrt3681A==");
            byte[] nonce = Encoding.UTF8.GetBytes("5I0FZhNXFDRpgtgR");

            byte[] bytes = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            Console.WriteLine("key='{0}'", Convert.ToBase64String(bytes));

            string[] list = {
                "Hello AES/GCM! Some non-standard chars: öçşığü",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                "DOGG74jC3Flrr3yH+3D",
                "Page_Login/txt_Password",
                "i7ovemydog!!",
                "C@ts-and-Dogs-Living-together",
                "1975"
            };

            foreach (string plainText in list)
            {
                Console.WriteLine("----------------------------------------------------------------------------");

                string inputString = plainText;
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                Console.WriteLine("plainText='{0}'", plainText);
                Console.WriteLine();

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
                cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), nonce));

                // Encrypt
                byte[] encryptedBytes = cipher.DoFinal(inputBytes);
                string base64EncryptedOutputString = Convert.ToBase64String(encryptedBytes);
                Console.WriteLine("encryptedBase64='{0}'", base64EncryptedOutputString);
                Console.WriteLine();

                // Decrypted
                byte[] toDecrypt = Convert.FromBase64String(base64EncryptedOutputString);
                cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), nonce));
                byte[] plainBytes = cipher.DoFinal(toDecrypt);
                Console.WriteLine("decryptedPlaintext='{0}'", Encoding.UTF8.GetString(plainBytes));
                Console.WriteLine();
            }
        }

        static void WriteByteArray(string name, byte[] byteArray)
        {
            Console.WriteLine("\"{0}\", {1} bytes, {2} bits:", name, byteArray.Length, byteArray.Length << 3);
            Console.WriteLine(BitConverter.ToString(byteArray));
            Console.WriteLine();
        }
    }
}
