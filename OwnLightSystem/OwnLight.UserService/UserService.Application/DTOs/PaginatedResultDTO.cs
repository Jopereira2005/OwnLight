namespace UserService.Application.DTOs;

public class PaginatedResultDTO(
    IEnumerable<UserResponseDTO> items,
    int totalCount,
    int page,
    int pageSize
)
{
    public int TotalCount { get; private set; } = totalCount;
    public int Page { get; private set; } = page;
    public int PageSize { get; private set; } = pageSize;
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public IEnumerable<UserResponseDTO> Items { get; private set; } = items;
}
