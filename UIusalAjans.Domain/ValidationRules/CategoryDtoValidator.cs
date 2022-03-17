using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlusalAjans.Domain.Dtos;

namespace UIusalAjans.Domain.ValidationRules
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
           
            RuleSet("update", () =>
            {
                RuleFor(x => x.Id).NotEmpty();
            });
        }
    }
}
