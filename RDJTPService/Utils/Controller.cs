using RDJTP.Core;
using RDJTP.Core.Extensions;
using System.Text.RegularExpressions;
using static RDJTP.Core.RequestMethodDefinitions;
using static RDJTP.Core.ResponseStatusDefinitions;

namespace RDJTPService.Utils
{
    public static class Controller
    {
        public static void ValidateRequest_AppendResponse(this Request request, Response response)
        {
            request.LowerRequestMethod();

            switch (request.Method)
            {
                case CREATE:

                    if (!request.IsReourceGiven() || !request.IsDateGiven() || !request.IsBodyGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);

                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(MISSING_BODY);
                    break;

                case READ:

                    if (!request.IsReourceGiven() || !request.IsDateGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);
                    break;

                case UPDATE:

                    if (!request.IsReourceGiven() || !request.IsDateGiven() || !request.IsBodyGiven() || !request.IsDateUnix())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);

                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(MISSING_BODY);

                    if (!request.IsDateUnix())
                        response.AddReasonPhrase(ILLEGAL_DATE);
                    break;

                case DELETE:

                    if (!request.IsReourceGiven() || !request.IsDateGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);
                    break;

                case ECHO:

                    if (!request.IsBodyGiven() || !request.IsDateGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(MISSING_BODY);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);
                    break;

                case null:

                    response.AddReasonPhrase(BADREQUEST_STATUS, MISSING_METHOD, MISSING_DATE);
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

        public static bool IsDateGiven(this Request request)
        {
            bool isGiven = false;

            if (!string.IsNullOrWhiteSpace(request.Date))
            {
                return !isGiven;
            }

            return isGiven;
        }

        public static bool IsDateUnix(this Request request)
        {
            Regex numericRegEx = new Regex("^(0|[1-9][0-9]*)$"); 

            bool isGiven = false;

            if (!string.IsNullOrWhiteSpace(request.Date) && numericRegEx.IsMatch(request.Date))
            {
                return !isGiven;
            }

            return isGiven;
        }
    }
}
