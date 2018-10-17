using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateSalesforceRESTApi
{
    public static class Constants
    {
        public static string USERNAME = "";
        public static string PASSWORD = "";
        public static string TOKEN = "";
        public static string CONSUMER_KEY = "";
        public static string CONSUMER_SECRET = "";
        public static string TOKEN_REQUEST_ENDPOINTURL = "https://login.salesforce.com/services/oauth2/token";
        public static string TOKEN_REQUEST_QUERYURL = "/services/data/v43.0/query?q=select+Id+,name+from+account+limit+10";

    }
}
