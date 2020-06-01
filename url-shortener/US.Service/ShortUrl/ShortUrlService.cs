using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using US.Domain.Repository.ShortUrl;
using US.IService.ShortUrl;
using US.IService.ShortUrl.DTOs;
using US.Service.Mapper;

namespace US.Service.ShortUrl
{
    public class ShortUrlService : IShortUrlService
    {
        private IShortUrlRepository _shortUrlRepository;

        public ShortUrlService(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public IEnumerable<ShortUrlDto> GetCollectionFromDataStore()
        {
            return _shortUrlRepository.GetCollectionFromDataStore().Result.Select(x => ShortUrlMapper.MapEntityToDto(x)).ToList();
        }

        public ShortUrlDto GetItemFromDataStore(string url)
        {
            return ShortUrlMapper.MapEntityToDto(_shortUrlRepository.GetItemFromDataStoreByShortUrl(url));
        }

        public ShortUrlResponseDto SaveItemToDataStore(ShortUrlRequestDto shortUrlRequest)
        {
            Domain.Entities.ShortUrl previouslySaved = _shortUrlRepository.GetItemFromDataStoreByLongUrl(shortUrlRequest.LongURL);
            if (previouslySaved != null)
            {
                return new ShortUrlResponseDto { ShortURL = previouslySaved.ShortURL, Success = true, Message = "This url has been saved previously" };
            }
            else
            {
                //if (LongUrlExists(shortUrlRequest.LongURL))
                //{
                //    string url = _shortUrlRepository.GetByFilter(x => x.LongURL == shortUrlRequest.LongURL).Result.FirstOrDefault().ShortURL;

                //    return new ShortUrlResponseDto
                //    {
                //        Message = "URL already exists",
                //        Success = true,
                //        ShortURL = url
                //    };
                //}

                Domain.Entities.ShortUrl shortUrl = ShortUrlMapper.MapRequestDtoToEntity(shortUrlRequest);

                var shorturl = GenerateShortUrl();

                if (ShortUrlExists(shorturl))
                {
                    while (!ShortUrlExists(shorturl))
                    {
                        shorturl = GenerateShortUrl();
                    }
                }

                shortUrl.ShortURL = shorturl;

                Domain.Entities.ShortUrl savedModel = _shortUrlRepository.SaveItemToDataStore(shortUrl).Result;

                try
                {
                    _shortUrlRepository.Commit();
                }
                catch (Exception ex)
                {
                    _shortUrlRepository.Rollback();
                    throw ex;
                }

                return new ShortUrlResponseDto
                {
                    ShortURL = savedModel.ShortURL,
                    Success = true,
                    Message = "Saved successfully"
                };
            }
        }

        public ShortUrlResponseDto SaveItemToDataStore(ShortUrlRequestDto shortUrlRequest, string shortUrl)
        {
            Domain.Entities.ShortUrl previouslySaved = _shortUrlRepository.GetItemFromDataStoreByLongUrl(shortUrlRequest.LongURL);
            if (previouslySaved != null)
            {
                return new ShortUrlResponseDto { ShortURL = previouslySaved.ShortURL, Success = true, Message = "This url has been saved previously" };
            }
            else
            {
                Domain.Entities.ShortUrl shorturl = ShortUrlMapper.MapRequestDtoToEntity(shortUrlRequest);

                if (ShortUrlExists(shortUrl))
                {
                    return new ShortUrlResponseDto
                    {
                        Message = "This short URL already exists, please pick another different.",
                        Success = false,
                        ShortURL = shortUrl
                    };
                }

                shorturl.ShortURL = shortUrl;

                Domain.Entities.ShortUrl savedModel = _shortUrlRepository.SaveItemToDataStore(shorturl).Result;

                try
                {
                    _shortUrlRepository.Commit();
                }
                catch (Exception)
                {
                    _shortUrlRepository.Rollback();
                    throw;
                }

                return new ShortUrlResponseDto
                {
                    ShortURL = savedModel.ShortURL,
                    Success = true,
                    Message = "Saved successfully"
                };
            }
        }

        private string GenerateShortUrl()
        {
            int length = 7;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }

        private bool ShortUrlExists(string url)
        {
            return _shortUrlRepository.GetByFilter(x => x.ShortURL.Equals(url)).Result.Any();
        }

        private bool LongUrlExists(string url)
        {
            return _shortUrlRepository.GetByFilter(x => x.LongURL.Equals(url)).Result.Any();
        }
    }
}
