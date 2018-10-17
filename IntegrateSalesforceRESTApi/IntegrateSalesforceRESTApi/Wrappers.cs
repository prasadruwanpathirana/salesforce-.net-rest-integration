using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IntegrateSalesforceRESTApi
{
    public class AuthResponse
    {
        public string access_token { get; set; }
        public string instance_url { get; set; }
        public string id { get; set; }
        public string token_type { get; set; }
        public string issued_at { get; set; }
        public string signature { get; set; }
    }
    public class Rootobject
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public Record[] records { get; set; }
    }
    public class Record
    {
        public Attributes attributes { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Attributes
    {
        public string type { get; set; }
        public string url { get; set; }
    }
}
