using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ALK.Core.Models;

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
}