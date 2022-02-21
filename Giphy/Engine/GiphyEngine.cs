using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Giphy.API
{
    public class GiphyEngine : IGiphyEngine
    {
        private string apiKey;
        private WebApiActivator webApiActivatior;
        private IMemoryCache memoryCache;
        private MemoryCacheHelper cacheHelper;


        public GiphyEngine(WebApiActivator oWebApiActivatior, MemoryCacheHelper oCacheHelper, IMemoryCache oMemoryCache)
        {
            apiKey = ConfigurationHelper.GetValue<string>("GiphyKey");
            webApiActivatior = oWebApiActivatior;
            cacheHelper = oCacheHelper;
            memoryCache = oMemoryCache;
    }
        public List<string> GetGiphyTrending(int? limit, string rating)
        {
            GiphyResponse giphyTrendy = new GiphyResponse();
            string url = GetApiUrl("TrendingGiphyUrl", limit, rating);
            giphyTrendy = webApiActivatior.Activate<GiphyResponse>(url);
            return giphyTrendy?.data?.Select(s => s.url).ToList();
        }

        public List<string> SearchGiphy(int? limit, string rating, string text)
        {
            GiphyResponse giphySearch = new GiphyResponse();
            string url = GetApiUrl("SearchGiphyUrl", limit, rating, text);
            //TODO: call to memoryCachgeHelper to get response
            //cacheHelper.GetResponse<GiphyResponse>(url, giphySearch => webApiActivatior.Activate<GiphyResponse>(url), giphySearch);
            bool isExists = memoryCache.TryGetValue<GiphyResponse>(url, out giphySearch);
            if (!isExists)
            {
                giphySearch = webApiActivatior.Activate<GiphyResponse>(url);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2));
                memoryCache.Set(url, giphySearch, cacheEntryOptions);
            }

            return giphySearch?.data?.Select(s => s.url).ToList();
        }

        private string GetApiUrl(string baseConfigUrl, int? limit, string rating = "", string text = "")
        {
            string url = string.Empty;
            string baseUrl = ConfigurationHelper.GetValue<string>(baseConfigUrl);
            string query = limit.HasValue ? $"&limit={limit.Value}" : "";
            query += !string.IsNullOrEmpty(rating) ? $"&rating={rating}" : "";
            query += !string.IsNullOrEmpty(text) ? $"&q={text}" : "";
            url = $"{baseUrl}?api_key={apiKey}{query}";
            return url;
        }


    }
}
