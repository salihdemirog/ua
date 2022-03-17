using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Dtos;

namespace UIusalAjans.Domain.ValidationRules
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Ürün adı zorunludur").Length(2,50);
            RuleFor(t => t.CategoryId).NotEmpty();
            RuleFor(t => t.UnitsInStock).NotEmpty();
            RuleFor(t => t.UnitPrice).NotEmpty().GreaterThan(0);
            RuleFor(t => t.QuantityPerUnit).Length(10, 250);

            When(t => t.UnitsInStock == 0, () =>
            {
                RuleFor(t => t.QuantityPerUnit).NotEmpty();
            });
        }
    }
}
