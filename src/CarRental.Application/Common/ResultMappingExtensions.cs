using AutoMapper;

namespace CarRental.Application.Common;

/// <summary>
/// Helper extensions to map result payloads while preserving success/failure semantics.
/// </summary>
public static class ResultMappingExtensions
{
    public static Result<TDestination> MapResult<TSource, TDestination>(this Result<TSource> source, Func<TSource, TDestination> mapper)
    {
        if (source.IsFailure)
        {
            return source.Errors.Count > 0
                ? Result<TDestination>.Failure(source.Errors)
                : Result<TDestination>.Failure(source.Error ?? "Operation failed.");
        }

        if (source.Value is null)
        {
            return Result<TDestination>.Failure("Operation result value was null.");
        }

        return Result<TDestination>.Success(mapper(source.Value));
    }

    public static Result<PaginatedList<TDestination>> MapPaginatedResult<TSource, TDestination>(
        this Result<PaginatedList<TSource>> source,
        Func<TSource, TDestination> mapper)
    {
        if (source.IsFailure)
        {
            return source.Errors.Count > 0
                ? Result<PaginatedList<TDestination>>.Failure(source.Errors)
                : Result<PaginatedList<TDestination>>.Failure(source.Error ?? "Operation failed.");
        }

        if (source.Value is null)
        {
            return Result<PaginatedList<TDestination>>.Failure("Operation result value was null.");
        }

        var mappedItems = source.Value.Items.Select(mapper).ToList();
        var paginated = new PaginatedList<TDestination>(
            mappedItems,
            source.Value.TotalCount,
            source.Value.PageNumber,
            source.Value.PageSize);

        return Result<PaginatedList<TDestination>>.Success(paginated);
    }

    public static Result<TDestination> MapResult<TSource, TDestination>(
        this Result<TSource> source,
        IMapper mapper)
    {
        return source.MapResult(value => mapper.Map<TDestination>(value));
    }

    public static Result<PaginatedList<TDestination>> MapPaginatedResult<TSource, TDestination>(
        this Result<PaginatedList<TSource>> source,
        IMapper mapper)
    {
        return source.MapPaginatedResult(value => mapper.Map<TDestination>(value));
    }
}
