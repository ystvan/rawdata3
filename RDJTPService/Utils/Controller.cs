using RDJTP.Core;
using RDJTP.Core.Extensions;
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
    }
}
