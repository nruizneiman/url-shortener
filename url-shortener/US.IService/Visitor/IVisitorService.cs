using System.Collections.Generic;
using System.Threading.Tasks;
using US.IService.Visitor.DTOs;

namespace US.IService.Visitor
{
    public interface IVisitorService
    {
        IEnumerable<VisitorDto> GetAll();
        Task<VisitorResponseDto> RegisterVisitor(VisitorRequestDto visitorRequest);
    }
}
