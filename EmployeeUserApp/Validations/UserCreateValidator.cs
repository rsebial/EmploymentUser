using EmployeeUserApp.Api.Dto;
using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Infrastructure;
using EmployeeUserApp.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EmployeeUserApp.Api.Validations;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    
    
    public UserCreateValidator(IEnumerable<User> users)
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(50);
       
    }


    
}
