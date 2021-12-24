using System;
using System.Security.Cryptography;

namespace BlogManagement.Common.Extensions
{
    /// <summary>
    /// This class stores string related extension methods.
    /// Contains:
    ///     + HashString.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// This method maps any string of any given length,
        /// to a string with a fixed length. This smaller, fixed length string is known as a hash.
        /// </summary>
        /// <param name="text">Text to map</param>
        /// <param name="salt">A private string that can be reliably retrieved when performing the hash in the future</param>
        /// <returns>A hashed string</returns>
        public static string HashString(string text, string salt = "")
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Uses SHA256 to create the hash
            using var sha = new SHA256Managed();

            // Convert the string to a byte array first, to be processed
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
            var hasBytes = sha.ComputeHash(textBytes);

            // Convert back to a string, removing the '-' that BitConverter adds
            var hash = BitConverter.ToString(hasBytes)
                .Replace("-", string.Empty);

            return hash;
        }
    }
}
