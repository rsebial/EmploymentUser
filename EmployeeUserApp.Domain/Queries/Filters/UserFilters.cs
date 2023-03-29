using EmployeeUserApp.Domain.Models;

namespace EmployeeUserApp.Domain.Queries.Filters;

/// <summary>
/// Builder pattern implementation for easy where clauses
/// Methods have simple implementation, and only 1 where if the variable has a query    
/// </summary>
internal static class UserFilters
{
    public static IQueryable<User> WhereFirstName(this IQueryable<User> query, string? firstName)
    {
        if (!string.IsNullOrWhiteSpace(firstName)) // condition to add the where
        {
            query = query.Where(x => x.FirstName.StartsWith(firstName)); // add where clause
        }

        return query; // single exit point
    }
    public static IQueryable<User> WhereEmail(this IQueryable<User> query, string? email)
    {
        if (!string.IsNullOrWhiteSpace(email)) // condition to add the where
        {
            query = query.Where(x => x.Email.StartsWith(email)); // add where clause
        }

        return query; // single exit point
    }

    public static IQueryable<User> WhereLastName(this IQueryable<User> query, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(lastName)) // condition to add the where
        {
            query = query.Where(x => x.LastName.StartsWith(lastName)); // add where clause
        }

        return query; // single exit point
    }

    public static IQueryable<User> WhereUserId(this IQueryable<User> query, long? userId)
    {
        if (userId > 0)
        {
            query = query.Where(x => x.Id == userId);
        }

        return query;
    }
}
