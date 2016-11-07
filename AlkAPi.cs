using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ALK.Core.Models;
using Newtonsoft.Json;

namespace ALK.Core
{
    public class AlkAPi : IAlkApi
    {
        public AlkAPi(string apiKey, string baseUri, string version)
        {
            ApiKey = apiKey;
            BaseUri = baseUri;
            Version = version;
            _baseUri = new Uri(BaseUri);
        }
        private Uri _baseUri { get; set; }
        public string ApiKey { get; set; }
        public string BaseUri { get; set; }
        public string Version { get; set; }

        public ReverseGeoCodeResponse ReverseGeoCode(double latitude, double longitude, bool matchedNamedRoadsOnly=true,
                int maxCleanupMiles=20, string region="NA", string dataset="Current")
            =>
            ReverseGeoCodeAsync(latitude, longitude, matchedNamedRoadsOnly, maxCleanupMiles, region, dataset)
                .GetAwaiter()
                .GetResult();
        public async Task<ReverseGeoCodeResponse> ReverseGeoCodeAsync(double latitude, double longitude, bool matchedNamedRoadsOnly, int maxCleanupMiles, string region, string dataset)
        {
            var res = new ReverseGeoCodeResponse();
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseUri;
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ApiKey);

            var param = new NameValueCollection();
            param.Add("coords",$"{longitude},{latitude}");
            param.Add(nameof(matchedNamedRoadsOnly),matchedNamedRoadsOnly.ToString());
            param.Add(nameof(maxCleanupMiles),maxCleanupMiles.ToString());
            param.Add(nameof(region),region);
            param.Add(nameof(dataset), dataset);

            var requestUri = new Uri(_baseUri,"locations");
            var requestUri2 = requestUri.AttachParameters(param);
            return await client.GetStringAsync(requestUri2).ContinueWith(task =>
            {
                var test =
                    (ReverseGeoCodeResponse[])
                    JsonConvert.DeserializeObject(task.Result, typeof(ReverseGeoCodeResponse[]));
                return test.FirstOrDefault();
            });
            
            
        }
    }
}