using FluentValidation;

namespace CarRental.Application.Features.Invoices.Commands.CreateInvoice;

/// <summary>
/// Validates the create Invoice command.
/// </summary>
public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInvoiceCommandValidator"/> class.
    /// </summary>
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to 0.");

        RuleFor(x => x.PaidAmount)
            .GreaterThanOrEqualTo(0).WithMessage("PaidAmount must be greater than or equal to 0.");

    }
}
