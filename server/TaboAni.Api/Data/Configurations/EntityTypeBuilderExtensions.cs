using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaboAni.Api.Data.Configurations;

internal static class EntityTypeBuilderExtensions
{
    internal static void ConfigureGuidKey<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, Guid>> keyExpression)
        where TEntity : class
    {
        var convertedExpression = Expression.Lambda<Func<TEntity, object?>>(
            Expression.Convert(keyExpression.Body, typeof(object)),
            keyExpression.Parameters);

        builder.HasKey(convertedExpression);
        builder.Property<Guid>(keyExpression).ValueGeneratedNever();
    }

    internal static PropertyBuilder<string> ConfigureRequiredVarchar<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, string>> propertyExpression,
        int maxLength)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasMaxLength(maxLength)
            .IsRequired();

    internal static PropertyBuilder<string?> ConfigureOptionalVarchar<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, string?>> propertyExpression,
        int maxLength)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasMaxLength(maxLength);

    internal static PropertyBuilder<string> ConfigureRequiredText<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, string>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("text")
            .IsRequired();

    internal static PropertyBuilder<string?> ConfigureOptionalText<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, string?>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("text");

    internal static PropertyBuilder<DateTimeOffset> ConfigureCreatedAt<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateTimeOffset>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

    internal static PropertyBuilder<DateTimeOffset> ConfigureUpdatedAt<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateTimeOffset>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

    internal static PropertyBuilder<DateTimeOffset> ConfigureTimestamp<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateTimeOffset>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("timestamp with time zone");

    internal static PropertyBuilder<DateTimeOffset?> ConfigureOptionalTimestamp<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateTimeOffset?>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("timestamp with time zone");

    internal static PropertyBuilder<DateOnly> ConfigureDate<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateOnly>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("date");

    internal static PropertyBuilder<DateOnly?> ConfigureOptionalDate<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, DateOnly?>> propertyExpression)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasColumnType("date");

    internal static PropertyBuilder<decimal> ConfigureDecimal<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, decimal>> propertyExpression,
        int precision,
        int scale)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasPrecision(precision, scale);

    internal static PropertyBuilder<decimal?> ConfigureOptionalDecimal<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, decimal?>> propertyExpression,
        int precision,
        int scale)
        where TEntity : class
        => builder.Property(propertyExpression)
            .HasPrecision(precision, scale);

    internal static void ApplySnakeCaseNaming(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.GetTableName() is { } tableName)
            {
                entityType.SetTableName(ToSnakeCase(tableName));
            }

            foreach (var property in entityType.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            foreach (var key in entityType.GetKeys())
            {
                if (key.GetName() is { } keyName)
                {
                    key.SetName(ToSnakeCase(keyName));
                }
            }

            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                if (foreignKey.GetConstraintName() is { } constraintName)
                {
                    foreignKey.SetConstraintName(ToSnakeCase(constraintName));
                }
            }

            foreach (var index in entityType.GetIndexes())
            {
                if (index.GetDatabaseName() is { } databaseName)
                {
                    index.SetDatabaseName(ToSnakeCase(databaseName));
                }
            }
        }
    }

    private static string ToSnakeCase(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var builder = new StringBuilder(value.Length + 8);

        for (var i = 0; i < value.Length; i++)
        {
            var current = value[i];

            if (char.IsUpper(current))
            {
                var hasPrevious = i > 0;
                var previous = hasPrevious ? value[i - 1] : '\0';
                var hasNext = i + 1 < value.Length;
                var next = hasNext ? value[i + 1] : '\0';

                if (hasPrevious &&
                    (char.IsLower(previous) || char.IsDigit(previous) || (char.IsUpper(previous) && hasNext && char.IsLower(next))))
                {
                    builder.Append('_');
                }

                builder.Append(char.ToLowerInvariant(current));
                continue;
            }

            builder.Append(current);
        }

        return builder.ToString();
    }
}
