using FluentValidation;
using FluentValidation.AspNetCore;
using PCShop.Models;

namespace PCShop.Validations
{
    public class ProductAttributeModelValidation : AbstractValidator<ProductAttributeViewModel>
    {
        public ProductAttributeModelValidation()
        {
            RuleFor(x => x.Value)
                .InclusiveBetween(1, 200)
                .When(x => !x.Name.Equals("OS"))
                .WithMessage("Not valid number");

            RuleFor(x => x.Value)
                .InclusiveBetween(1, 2)
                .When(x => x.Name.Equals("OS"))
                .WithMessage("Not valid number for OS");
        }
    }
}