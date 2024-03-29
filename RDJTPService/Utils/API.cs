﻿using Newtonsoft.Json.Linq;
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

                    try
                    {
                        if (request.Path.ToLower() == "/api/categories")
                        {
                            var categoryForCreation = request.Body.FromJson<Category>();
                            CreateCategory(categoryForCreation);

                            var categories = GetCategories();
                            var match = categories.FirstOrDefault(c => c.Name.ToLower() == categoryForCreation.Name.ToLower());

                            response.Body = match.ToJson();
                            response.AddReasonPhrase(CREATED_STATUS);
                            break;
                        }
                        else
                        {
                            response.AddReasonPhrase(BADREQUEST_STATUS);
                        }
                    }
                    catch (Exception)
                    {
                        response.AddReasonPhrase(BADREQUEST_STATUS);
                    }

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

                    try
                    {
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
                                requestedCategory.Name = request.Body.FromJson<Category>().Name;
                                response.AddReasonPhrase(UPDATED_STATUS);
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

                case DELETE:

                    if (!request.IsReourceGiven() || !request.IsDateGiven())
                        response.AddReasonPhrase(BADREQUEST_STATUS);

                    if (!request.IsReourceGiven())
                        response.AddReasonPhrase(MISSING_RESOURCE);

                    if (!request.IsDateGiven())
                        response.AddReasonPhrase(MISSING_DATE);

                    try
                    {
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
                                if (DeleteCategory(requestedCategory.Id))
                                {
                                    response.AddReasonPhrase(OK_STATUS);
                                }

                                response.AddReasonPhrase(NOTFOUND_STATUS);
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
            return Server.categories;
        }

        private static void CreateCategory(Category category)
        {
            Server.categories.Add(new Category { Id = 4, Name = category.Name});            
        }

        private static bool DeleteCategory(int categoryId)
        {
            bool success = true;

            var match = Server.categories.FirstOrDefault(c => c.Id == categoryId);
            if (match != null)
            {
                Server.categories.Remove(match);
                return success;
            }
            else
            {
                return !success;
            }
        }
    }
}
