namespace DeviceService.Application.DTOs;

public class PaginatedResultDTO<T>(int totalCount, int page, int pageSize, IEnumerable<T> items)
{
    public int TotalCount { get; set; } = totalCount;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public IEnumerable<T> Items { get; set; } = items;
}
