using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Giphy.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiphyController : ControllerBase
    {
        public IGiphyEngine IEngine;

        public GiphyController(IGiphyEngine engine)
        {
            IEngine = engine;
        }

        [HttpGet]
        public List<string> GetGiphyTrending(int? limit, string rating)
        {
            return IEngine.GetGiphyTrending(limit, rating);
        }

        [HttpGet]
        public List<string> SearchGiphy(int? limit, string rating, string text)
        {
            return IEngine.SearchGiphy(limit, rating, text);
        }
    }
}
