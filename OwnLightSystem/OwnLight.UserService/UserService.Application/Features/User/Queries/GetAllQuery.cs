using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Features.User.Queries;

public class GetAllQuery(int page, int pageSize) : IRequest<PaginatedResultDTO>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}
