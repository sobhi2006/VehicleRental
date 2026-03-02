using FluentValidation;

namespace CarRental.Application.Features.Invoices.Commands.UpdateInvoice;

/// <summary>
/// Validates the update Invoice command.
/// </summary>
public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInvoiceCommandValidator"/> class.
    /// </summary>
    public UpdateInvoiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to 0.");

        RuleFor(x => x.PaidAmount)
            .GreaterThanOrEqualTo(0).WithMessage("PaidAmount must be greater than or equal to 0.");

    }
}
