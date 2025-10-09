namespace Shared.Dtos;

public class MovieParameterSpecification
{
    private const int MaxPageSize = 10;
    private const int DefaultPageSize = 5;
    public string? Search { get; set; }
    public string? Genre { get; set; } 
    public decimal? Rating  { get; set; }
    public DateOnly? ExactReleaseDate { get; set; }
    
    public DateOnly? MinReleaseDate { get; set; }
    
    public DateOnly? MaxReleaseDate { get; set; }
    public MovieSortOptions? Sort   { get; set; }
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
    public enum MovieSortOptions
    {
        NameAsc,
        NameDesc
    }
}