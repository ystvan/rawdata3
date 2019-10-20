using RDJTP.Core;
using RDJTP.Core.Extensions;
using System;
using static RDJTP.Core.RequestMethodDefinitions;
using static RDJTP.Core.ResponseStatusDefinitions;

namespace RDJTPService.Utils
{
    public static class Controller
    {
        public static void ValidateRequestAndAddResponse(this Request request, Response response)
        {
            request.LowerRequestMethod();

            switch (request.Method)
            {
                case CREATE:
                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_RESOURCE);
                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_BODY);
                    break;
                case READ:
                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_RESOURCE);
                    break;
                case UPDATE:
                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_RESOURCE);
                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_BODY);
                    break;
                case DELETE:
                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_RESOURCE);
                    break;
                case ECHO:
                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_BODY);
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

        public static bool IsReourceGiven(this Request request)
        {
            bool isGiven = false;

            if (!string.IsNullOrWhiteSpace(request.Path))
            {
                return !isGiven;
            }

            return isGiven;
        }

        public static bool IsBodyGiven(this Request request)
        {
            bool isGiven = false;

            if (!string.IsNullOrWhiteSpace(request.Body))
            {
                return !isGiven;
            }

            return isGiven;
        }
    }
}
