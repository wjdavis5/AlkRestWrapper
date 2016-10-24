using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ALK.Core.Models;
using Newtonsoft.Json;

namespace ALK.Core
{
    public interface IAlkApi
    {
        string ApiKey { get; set; }
        string BaseUri { get; set; }
        string Version { get; set; }

        Task<ReverseGeoCodeResponse> ReverseGeoCodeAsync(double latitude, double longitude, bool matchedNamedRoadsOnly, int maxCleanupMiles, string region, string dataset);
        ReverseGeoCodeResponse ReverseGeoCode(double latitude, double longitude, bool matchedNamedRoadsOnly, int maxCleanupMiles, string region, string dataset);
    }

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

        public ReverseGeoCodeResponse ReverseGeoCode(double latitude, double longitude, bool matchedNamedRoadsOnly,
                int maxCleanupMiles, string region, string dataset)
            =>
            ReverseGeoCodeAsync(latitude, longitude, matchedNamedRoadsOnly, maxCleanupMiles, region, dataset)
                .GetAwaiter()
                .GetResult();
        public Task<ReverseGeoCodeResponse> ReverseGeoCodeAsync(double latitude, double longitude, bool matchedNamedRoadsOnly, int maxCleanupMiles, string region, string dataset)
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
            return client.GetStringAsync(requestUri2).ContinueWith(task =>
            {
                var test =
                    (ReverseGeoCodeResponse[])
                    JsonConvert.DeserializeObject(task.Result, typeof(ReverseGeoCodeResponse[]));
                return test.FirstOrDefault();
            });
            
            
        }
    }

    public static class UriExt
    {
        public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";
            for (int index = 0; index < parameters.Count; ++index)
            {
                
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + WebUtility.UrlEncode(parameters[index]));
                str = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }
    }

}