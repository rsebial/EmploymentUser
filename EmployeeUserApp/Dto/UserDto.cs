using EmployeeUserApp.Domain.Models;
using System.Collections.Generic;

namespace EmployeeUserApp.Api.Dto;

public record UserSearchDto(long Id, string FirstName, string LastName);
public record UserQueryDto(string? FirstName, string? LastName);
public record UserIdQueryDto(long? Id);
public record UserGetDto(long Id, string FirstName, string LastName, string? Email, Address? Address, List<Employment>? Employments);

public record UserGetAllDto(long Id, string? FirstName, string? LastName, string? Email, Address? Address, List<Employment>? Employments);
public record UserCreateDto(string? FirstName, string? LastName, string? Email, Address? Address, List<Employment>? Employments);
public record UserUpdateDto(long Id, string? FirstName, string? LastName, string? Email, Address? Address, List<Employment>? Employments);
public record UserDeleteDto(long Id);

