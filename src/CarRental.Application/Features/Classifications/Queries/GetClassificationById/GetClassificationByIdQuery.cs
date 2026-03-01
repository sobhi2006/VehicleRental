using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;

namespace CarRental.Application.Features.Classifications.Queries.GetClassificationById;

/// <summary>
/// Query to get a Classification by id.
/// </summary>
/// <param name="Id">Identifier of the Classification.</param>
public record GetClassificationByIdQuery(long Id) : IRequest<Result<ClassificationDto>>;
