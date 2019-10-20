namespace RDJTP.Core
{
    public static class ResponseStatusDefinitions
    {
        // Status codes
        public const string OK_STATUS =         "1 Ok ";
        public const string CREATED_STATUS =    "2 Created ";
        public const string UPDATED_STATUS =    "3 Updated ";
        public const string BADREQUEST_STATUS = "4 Bad Request ";
        public const string NOTFOUND_STATUS =   "5 Not Found ";
        public const string ERROR_STATUS =      "6 Error ";

        // Missing reasons phrases with elements
        public const string MISSING_DATE =       "missing date ";
        public const string MISSING_BODY =       "missing body ";
        public const string MISSING_METHOD =     "missing method ";
        public const string MISSING_RESOURCE =   "missing resource ";

        // Illegal reasons phrases with elements
        public const string ILLEGAL_DATE =   "illegal date ";
        public const string ILLEGAL_BODY =   "illegal body ";
        public const string ILLEGAL_PATH =   "illegal path ";
        public const string ILLEGAL_METHOD = "illegal method ";

    }
}
