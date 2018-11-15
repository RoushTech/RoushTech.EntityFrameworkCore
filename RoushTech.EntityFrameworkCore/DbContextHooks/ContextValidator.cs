namespace RoushTech.EntityFrameworkCore.DbContextHooks
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class ContextValidator : IDbContextValidator
    {
        public void Validate(object entity, IProperty property)
        {
            ValidateMaxLength(entity, property);
        }

        protected void ValidateMaxLength(object entity, IProperty property)
        {
            var maxLength = property.GetMaxLength();
            if (maxLength == null)
            {
                return;
            }

            if (!(property.PropertyInfo.GetValue(entity) is string value))
            {
                return;
            }

            if (maxLength < value.Length)
            {
                throw new InvalidOperationException(
                    $"{entity.GetType()}.{property.Name} data will be truncated. Max: {maxLength} Required: {value.Length}");
            }
        }
    }
}