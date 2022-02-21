using System.Collections.Generic;

namespace Giphy.API
{
    public interface IGiphyEngine
    {
        List<string> GetGiphyTrending(int? limit, string rating);

        List<string> SearchGiphy(int? limit, string rating, string text);
    }
}
