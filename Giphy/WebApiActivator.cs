using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Giphy.API
{
    public class WebApiActivator
    {
        public T Activate<T>(string url)
        {
            T response;
            using (HttpClient httpClient = new HttpClient())
            {
                using (var result = httpClient.GetAsync(url).Result)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        string json = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<T>(json);
                    }
                    else
                        throw new Exception($"Activate Web Api return Http error. url: {url} errorCode: {result.StatusCode} ReasonPhrase: {result.ReasonPhrase}");
                }
            }
            return response;
        }
    }
}


