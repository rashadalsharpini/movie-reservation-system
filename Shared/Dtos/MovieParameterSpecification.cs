namespace Shared.Dtos;

public class MovieParameterSpecification
{
    private const int MaxPageSize = 10;
    private const int DefaultPageSize = 5;
    public string? Search { get; set; }
    public int PageIndex { get; set; } = 1;
    private int _pageSize=DefaultPageSize;
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize=value>MaxPageSize?MaxPageSize:value;
        }
    }
}