using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;

namespace CarRental.Application.Features.BlockListCustomers.Queries.GetBlockListCustomerById;

/// <summary>
/// Query to get a BlockListCustomer by id.
/// </summary>
/// <param name="Id">Identifier of the BlockListCustomer.</param>
public record GetBlockListCustomerByIdQuery(long Id) : IRequest<Result<BlockListCustomerDto>>;
