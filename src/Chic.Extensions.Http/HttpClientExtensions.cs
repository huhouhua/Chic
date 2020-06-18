using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chic.Extensions.Http
{
    public static class HttpClientExtensions
    {

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, Uri requestUrl, T data, CancellationToken cancellationToken)
        {
            string json = JsonConvert.SerializeObject(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(requestUrl, content, cancellationToken);

        }


    }
}
