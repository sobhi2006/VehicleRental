using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Invoices.Commands.CreateInvoice;

/// <summary>
/// Validates the create Invoice command.
/// </summary>
public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    private readonly IBookingVehicleService _bookingVehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInvoiceCommandValidator"/> class.
    /// </summary>
    public CreateInvoiceCommandValidator(IBookingVehicleService bookingVehicleService)
    {
        _bookingVehicleService = bookingVehicleService;
        ApplyRules();
        ApplyCustomRules();
    }

    private void ApplyRules()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.IssueDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("IssueDate cannot be in the future.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("TotalAmount must be greater than 0.");

        RuleFor(x => x.InvoiceLines)
            .NotNull().WithMessage("InvoiceLines cannot be null.");

        RuleForEach(x => x.InvoiceLines)
            .ChildRules(line =>
            {
                line.RuleFor(l => l.Description)
                    .NotEmpty().WithMessage("Invoice line description is required.")
                    .MaximumLength(500).WithMessage("Invoice line description must not exceed 500 characters.");

                line.RuleFor(l => l.Quantity)
                    .GreaterThan(0).WithMessage("Invoice line quantity must be greater than 0.");

                line.RuleFor(l => l.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Invoice line unit price must be greater than or equal to 0.");
            });
    }

    private void ApplyCustomRules()
    {
        RuleFor(x => x.BookingId)
            .MustAsync(async (bookingId, cancellationToken) =>
            {
                return await _bookingVehicleService.ExistsByIdAsync(bookingId, cancellationToken);
            })
            .WithMessage("Booking not found.");
    }
}
