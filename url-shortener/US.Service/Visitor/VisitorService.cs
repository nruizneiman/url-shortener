using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using US.Domain.Repository.ShortUrl;
using US.Domain.Repository.Visitor;
using US.IService.Visitor;
using US.IService.Visitor.DTOs;

namespace US.Service.Visitor
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IShortUrlRepository _shortUrlRepository;

        public VisitorService(IVisitorRepository visitorRepository, IShortUrlRepository shortUrlRepository)
        {
            _visitorRepository = visitorRepository;
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<IEnumerable<VisitorDto>> GetAll()
        {
            var visitors = await _visitorRepository.GetByFilter(includeProperties: "ShortUrl");

            return visitors.Select(x => new VisitorDto
            {
                Id = x.Id,
                Date = x.Date,
                Ip = x.Ip,
                UserAgent = x.UserAgent,
                LongURL = x.ShortUrl.LongURL,
                ShortURL = x.ShortUrl.ShortURL
            }).ToList();
        }

        public async Task<VisitorResponseDto> RegisterVisitor(VisitorRequestDto visitorRequest)
        {
            Domain.Entities.ShortUrl shortUrl = _shortUrlRepository.GetByFilter(x => x.ShortURL == visitorRequest.ShortUrl).Result.FirstOrDefault();

            var newVisitor = new Domain.Entities.Visitor
            {
                Date = DateTime.Now,
                IsDeleted = false,
                Ip = visitorRequest.Ip,
                UserAgent = visitorRequest.UserAgent,
                ShortUrl = shortUrl
            };

            await _visitorRepository.Create(newVisitor);

            try
            {
                _visitorRepository.Commit();

                return new VisitorResponseDto
                {
                    Message = "Visitor registered",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _visitorRepository.Rollback();
                throw ex;
            }
        }
    }
}
