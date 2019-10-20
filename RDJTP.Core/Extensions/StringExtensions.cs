using RDJTP.Core;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RDJTP.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Generic method to deserialize JSON string to type T
        /// </summary>
        /// <typeparam name="T">Type to deserialize</typeparam>
        /// <param name="element">JSON string</param>
        /// <returns></returns>
        public static T FromJson<T>(this string element) where T : new()
        {
            return JsonSerializer.Deserialize<T>(element, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        /// <summary>
		/// Throws ArgumentOutOfRangeException if parameter is null or whitespace.
		/// </summary>
		/// <param name="parameterName">Parameter name used for exception message.</param>
		public static void ThrowIfParameterIsNullOrWhiteSpace(this string parameter, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentOutOfRangeException(parameterName, "Cannot be null or whitespace.");
            }
        }

        public static bool GetCategoryIdFromPathIfExist(this string fullPath, out int id) 
        {
            string[] elements = fullPath.Split("/api/categories/");

            bool isThereAnId = int.TryParse(elements[1], out id);

            return isThereAnId;              
        }

        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
}
