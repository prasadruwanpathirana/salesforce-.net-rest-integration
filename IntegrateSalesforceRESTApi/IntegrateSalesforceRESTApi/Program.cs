using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace IntegrateSalesforceRESTApi
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Task<AuthResponse> authResponse = Task.Run(() => Program.AsyncAuthRequest());
            authResponse.Wait();
            if (authResponse.Result != null)
            {
                Task<Rootobject> leadResponse = Task.Run(() => Program.AsyncQueryRequest(authResponse.Result.access_token, authResponse.Result.instance_url));
                leadResponse.Wait();
            }
        }
        public async static Task<AuthResponse> AsyncAuthRequest()
        {
            var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", Constants.CONSUMER_KEY),
                    new KeyValuePair<string, string>("client_secret", Constants.CONSUMER_SECRET),
                    new KeyValuePair<string, string>("username", Constants.USERNAME),
                    new KeyValuePair<string, string>("password", Constants.PASSWORD + Constants.TOKEN)
                });
            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.TOKEN_REQUEST_ENDPOINTURL),
                Content = content
            };
            var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            AuthResponse responseDyn = JsonConvert.DeserializeObject<AuthResponse>(response);
            return responseDyn;
        }
        public async static Task<Rootobject> AsyncQueryRequest(string token, string url)
        {
            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + Constants.TOKEN_REQUEST_QUERYURL),
                Content = null
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await new HttpClient().SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Deserialize<Rootobject>(response);
            foreach (var rootObj in serializedResult.records)
            {
                Record leadRecord = rootObj;
                Console.WriteLine(leadRecord.Id + " " + leadRecord.Name);
            }
            return serializedResult;
    }
}
}
