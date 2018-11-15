namespace RoushTech.EntityFrameworkCore.DbContextHooks
{
    using Microsoft.EntityFrameworkCore.Metadata;

    public interface IDbContextValidator
    {
        void Validate(object entity, IProperty property);
    }
}