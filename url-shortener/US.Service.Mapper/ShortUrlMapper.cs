using System;
using US.Domain.Entities;
using US.IService.ShortUrl.DTOs;

namespace US.Service.Mapper
{
    public static class ShortUrlMapper
    {
        public static ShortUrl MapRequestDtoToEntity(ShortUrlRequestDto shortUrlRequestDto)
        {
            ShortUrl result = new ShortUrl
            {
                LongURL = shortUrlRequestDto.LongURL,
                CreationDate = DateTime.Now
            };

            return result;
        }

        public static ShortUrlDto MapEntityToDto(ShortUrl shortUrl)
        {
            return new ShortUrlDto
            {
                Id = shortUrl.Id,
                CreationDate = shortUrl.CreationDate,
                ShortURL = shortUrl.ShortURL,
                LongURL = shortUrl.LongURL
            };
        }
    }
}
