namespace CarRental.Application.Common;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
public class PaginatedList<T>
{
    /// <summary>
    /// Gets the items on the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; }
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber { get; }
    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    public int PageSize { get; }
    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalCount { get; }
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }
    /// <summary>
    /// Gets a value indicating whether a previous page exists.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;
    /// <summary>
    /// Gets a value indicating whether a next page exists.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    public PaginatedList(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
