using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using US.Domain.Base.IRepository;
using US.IService.ShortUrl;
using US.IService.ShortUrl.DTOs;
using US.Service.Mapper;

namespace US.Service.ShortUrl
{
    public class ShortUrlService : IShortUrlService
    {
        private IRepository shortUrlRepository;

        public ShortUrlService(IRepository repository)
        {
            shortUrlRepository = repository;
        }

        public string GenerateShortUrl()
        {
            string urlsafe = string.Empty;
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(i));
            string token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));

            return token;
        }

        public IEnumerable<ShortUrlDto> GetCollectionFromDataStore()
        {
            return shortUrlRepository.GetCollectionFromDataStore().Select(x => ShortUrlMapper.MapEntityToDto(x)).ToList();
        }

        public ShortUrlDto GetItemFromDataStore(string url)
        {
            return ShortUrlMapper.MapEntityToDto(shortUrlRepository.GetItemFromDataStoreByShortUrl(url));
        }

        public ShortUrlResponseDto SaveItemToDataStore(ShortUrlRequestDto shortUrlRequest)
        {
            Domain.Entities.ShortUrl previouslySaved = shortUrlRepository.GetItemFromDataStoreByLongUrl(shortUrlRequest.LongURL);
            if (previouslySaved != null)
            {
                return new ShortUrlResponseDto { ShortURL = previouslySaved.ShortURL, Success = true, Message = "This url has been saved previously" };
            }
            else
            {
                Domain.Entities.ShortUrl shortUrl = ShortUrlMapper.MapRequestDtoToEntity(shortUrlRequest);

                shortUrl.ShortURL = GenerateShortUrl();

                Domain.Entities.ShortUrl savedModel = shortUrlRepository.SaveItemToDataStore(shortUrl);

                return new ShortUrlResponseDto
                {
                    ShortURL = savedModel.ShortURL,
                    Success = true,
                    Message = "Saved successfully"
                };
            }
        }
    }
}
