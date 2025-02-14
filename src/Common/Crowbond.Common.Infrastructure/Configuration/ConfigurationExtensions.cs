using System.Linq.Expressions;
using Crowbond.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Crowbond.Common.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        // Try the direct path first (how we set it)
        string? connString = configuration[$"ConnectionStrings:{name}"];
        
        // Fall back to GetConnectionString if not found
        connString ??= configuration.GetConnectionString(name);
        
        return connString ?? 
               throw new InvalidOperationException($"The connection string {name} was not found");
    }

    public static T GetValueOrThrow<T>(this IConfiguration configuration, string name)
    {
        return configuration.GetValue<T?>(name) ??
               throw new InvalidOperationException($"The connection string {name} was not found");
    }

    public static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                Type clrType = entityType.ClrType;

                ParameterExpression parameter = Expression.Parameter(clrType, "e");
                MemberExpression property = Expression.Property(parameter, "IsDeleted");
                LambdaExpression filter = Expression.Lambda(
                    Expression.Equal(property, Expression.Constant(false)), parameter);

                modelBuilder.Entity(clrType).HasQueryFilter(filter);
            }
        }
    }
}
