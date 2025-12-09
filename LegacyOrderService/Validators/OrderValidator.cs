using FluentValidation;
using LegacyOrderService.Models;

namespace LegacyOrderService.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerName)
                .NotEmpty().WithMessage("Customer name cannot be empty.");

            RuleFor(o => o.ProductName)
                .NotEmpty().WithMessage("Product name cannot be empty.");

            RuleFor(o => o.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(o => o.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}