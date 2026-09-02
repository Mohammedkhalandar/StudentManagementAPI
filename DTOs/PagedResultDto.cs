namespace StudentManagementAPI.DTOs;

public class PagedResultDto<T>
{
    public int TotalRecords { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<T> Data { get; set; } = new List<T>();
}