using ECommerceAPI.Persistence.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products;

public class CreateProductValidator: AbstractValidator<VmCreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(150)
            .MinimumLength(2)
            .WithMessage("Name must be between 2 and 150 characters");
        
        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .MinimumLength(10)
            .WithMessage("Description must be between 10 and 500 characters");

        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Stock is required")
            .GreaterThan(0)
            .WithMessage("Stock must be greater than 0");
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
        
    }
}