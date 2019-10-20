using System;
using System.Text.Json;
using static RDJTP.Core.RequestMethodDefinitions;
using static RDJTP.Core.ResponseStatusDefinitions;

namespace RDJTP.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void ValidateRequestAndAddResponse(this Request request, Response response) 
        {
            request.LowerRequestMethod();

            switch (request.Method)
            {
                case CREATE:
                    break;
                case READ:
                    break;
                case UPDATE:
                    break;
                case DELETE:
                    break;
                case ECHO:
                    break;
                case null:
                    response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_METHOD);
                    break;
                default:
                    response.AddReasonPhrase(BADREQUEST_STATUS, ILLEGAL_METHOD);
                    break;
            }             
        }

        public static void AddReasonPhrase(this Response response, params string[] reasons) 
        {
            foreach (var item in reasons)
            {
                response.Status += item;
            }
        }

        public static void LowerRequestMethod(this Request request) 
        {
            request.Method?.ToLower();        
        }

        /// <summary>
        /// Serialize of type object into JSON
        /// </summary>
        /// <param name="data">The object</param>
        /// <returns>JSON string in camel case</returns>
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        /// <summary>
		/// Throws ArgumentNullException if parameter is null.
		/// </summary>
		/// <param name="parameterName">Parameter name used for exception message.</param>
		public static void ThrowIfParameterIsNull(this object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws NullReferenceException if object is null.
        /// </summary>
        public static void ThrowIfObjectIsNull(this object obj, string objectName, string message = null)
        {
            if (obj == null)
            {
                string msg = string.Empty;

                if (!string.IsNullOrWhiteSpace(message))
                {
                    msg = " " + message;
                }

                throw new NullReferenceException($"{objectName} is null.{msg}");
            }
        }
    }
}
