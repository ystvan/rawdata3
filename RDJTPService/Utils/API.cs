using Newtonsoft.Json.Linq;
using RDJTP.Core;
using RDJTP.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static RDJTP.Core.RequestMethodDefinitions;
using static RDJTP.Core.ResponseStatusDefinitions;

namespace RDJTPService.Utils
{
    public static class API
    {
        private static int Id = 0;

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

                    try
                    {
                        if (request.Path.ToLower() == "/api/categories")
                        {
                            var categories = GetCategories();
                            response.AddBody(categories.ToJson());
                            response.AddReasonPhrase(OK_STATUS);
                            break;
                        }

                        if (!request.Path.GetCategoryIdFromPathIfExist(out Id))
                        {
                            response.AddReasonPhrase(BADREQUEST_STATUS);
                        }
                        else if (Id != 0)
                        {
                            var categories = GetCategories();
                            var requestedCategory = categories.FirstOrDefault(c => c.Id == Id);
                            if (requestedCategory != null)
                            {
                                response.AddReasonPhrase(OK_STATUS);
                                response.AddBody(requestedCategory.ToJson());
                            }
                            else
                            {
                                response.AddReasonPhrase(NOTFOUND_STATUS);
                            }
                            
                        }
                    }
                    catch (Exception)
                    {
                        response.AddReasonPhrase(BADREQUEST_STATUS);
                    }
                    
                    break;

                case UPDATE:

                    if (!request.IsReourceGiven() || !request.IsDateGiven() || !request.IsBodyGiven() || !request.IsDateUnix() || !request.IsBodyJson())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);

                    if (!request.IsBodyGiven())
                        response.AddReasonPhrase(MISSING_BODY);

                    if (!request.IsDateUnix())
                        response.AddReasonPhrase(ILLEGAL_DATE);

                    if (!request.IsBodyJson())
                        response.AddReasonPhrase(ILLEGAL_BODY);

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
                    {
                        response.AddReasonPhrase(MISSING_BODY);
                    }
                    else
                    {
                        response.Body = request.Body;
                    }

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

        public static void AddBody(this Response response, string body)
        {
            response.Body = body;
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

        public static bool IsBodyJson(this Request request)
        {
            bool isJson = false;

            try
            {
                var obj = JToken.Parse(request.Body);
                return !isJson;
            }
            catch (Exception)
            {
                return isJson;
            }
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Beverages" },
                new Category { Id = 2, Name = "Condiments" },
                new Category { Id = 3, Name = "Confections" },
            };

            return categories;
        }
    }
}
