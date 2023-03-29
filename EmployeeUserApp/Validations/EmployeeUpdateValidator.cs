using EmployeeUserApp.Api.Dto;
using FluentValidation;

namespace EmployeeUserApp.Api.Validations;

public class EmployeeUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(50);
    }
}
