using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using GRomash.CrmWebApiEarlyBoundGenerator.Controls;
using McTools.Xrm.Connection;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions
{
    public static class ConnectionDetailsExtensions
    {
         /// <summary>
        /// Attempts to lookup the user password.  First by reflection of the userPassword, then by the old public property, then by a config value, then by crying uncle and prompting the user for the password 
        /// </summary>
        /// <returns></returns>
        public static string GetUserPassword(this ConnectionDetail connection)
        {
            try
            {
                if (connection.PasswordIsEmpty)
                {
                    return string.Empty;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Probably a pervious version of the XTB.  Attempt to soldier on...
            }

            var field = connection.GetType().GetField("userPassword", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
            {
                return Decrypt((string) field.GetValue(connection), "MsCrmTools", "Tanguy 92*", "SHA1", 2, "ahC3@bCa2Didfc3d", 256);
            }

            // Lookup Old Public Property
            var prop = connection.GetType().GetProperty("UserPassword", BindingFlags.Instance | BindingFlags.Public);
            if (prop != null)
            {
                return (string)prop.GetValue(connection);
            }

            // Lookup Config Value
            var password = ConfigurationManager.AppSettings["EarlyBoundGenerator.CrmSvcUtil.UserPassword"];
            if (!string.IsNullOrWhiteSpace(password))
            {
                return password;
            }

            MessageBox.Show(@"Unable to find ""EarlyBoundGenerator.CrmSvcUtil.UserPassword"" in app.config.");

            // Ask User for value
            while (string.IsNullOrWhiteSpace(password))
            {
                password = Prompt.ShowDialog("Please enter your password:", "Enter Password");
            }
            return password;
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="saltValue">The salt value.</param>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="passwordIterations">The password iterations.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
         {
             var bytes1 = Encoding.ASCII.GetBytes(initVector);
             var bytes2 = Encoding.ASCII.GetBytes(saltValue);
             var buffer = Convert.FromBase64String(cipherText);
             var bytes3 = new PasswordDeriveBytes(passPhrase, bytes2, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
             var rijndaelManaged = new RijndaelManaged {Mode = CipherMode.CBC};
             var decryptor = rijndaelManaged.CreateDecryptor(bytes3, bytes1);
             var memoryStream = new MemoryStream(buffer);
             var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
             var numArray = new byte[buffer.Length];
             var count = cryptoStream.Read(numArray, 0, numArray.Length);
             memoryStream.Close();
             cryptoStream.Close();
             return Encoding.UTF8.GetString(numArray, 0, count);
         }
    }
}
