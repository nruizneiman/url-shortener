using System.Collections.Generic;
using US.IService.ShortUrl.DTOs;

namespace US.IService.ShortUrl
{
    public interface IShortUrlService
    {
        IEnumerable<ShortUrlDto> GetCollectionFromDataStore();
        ShortUrlDto GetItemFromDataStore(string url);
        ShortUrlResponseDto SaveItemToDataStore(ShortUrlRequestDto shortUrlRequest);
    }
}
