using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Launchpad.Core.Factories
{
    public interface IHttpClientFactory
    {
        HttpClient Create(string endpoint);
    }

    public class HttpClientFactory : IHttpClientFactory
    {
        private static readonly IDictionary<string, HttpClient> HttpClients = new ConcurrentDictionary<string, HttpClient>();

        public HttpClient Create(string endpoint)
        {
            if (HttpClients.TryGetValue(endpoint, out HttpClient client))
            {
                return client;
            }

            client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
            HttpClients[endpoint] = client;

            return client;
        }
    }

    public static class HttpClientFactoryExtensions
    {
        /// <summary>
        /// Return the HttpClient response as an instance of the specified type.
        /// </summary>
        public static async Task<T> ConvertResponseToObject<T>(this HttpResponseMessage responseMessage)
        {
            var responseAsString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(responseAsString);
        }
    }
}
